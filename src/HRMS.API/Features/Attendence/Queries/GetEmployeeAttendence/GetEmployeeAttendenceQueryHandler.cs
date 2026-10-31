using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Features.Attendence.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Attendence.Queries.GetEmployeeAttendence;

public class GetEmployeeAttendenceHandler(AppDbContext context)
    : IRequestHandler<
        GetEmployeeAttendenceQuery,
        Result<List<GetEmployeeAttendenceResponse>>>
{
    public async Task<Result<List<GetEmployeeAttendenceResponse>>> Handle(
        GetEmployeeAttendenceQuery request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var currentTime = TimeOnly.FromDateTime(now);

        // 1) Schedules are the source of truth (up to today only,
        //    future shifts must not be counted as absent).
        var schedules = await context.EmployeeSchedules
            .AsNoTracking()
            .Include(s => s.Shift)
            .Where(s =>
                s.EmployeeId == request.EmployeeId &&
                s.Date <= today)
            .OrderByDescending(s => s.Date)
            .ToListAsync(cancellationToken);

        if (schedules.Count == 0)
        {
            return Result<List<GetEmployeeAttendenceResponse>>
                .Success([]);
        }

        var scheduleDates = schedules
            .Select(s => s.Date)
            .Distinct()
            .ToList();

        // 2) Attendance for those dates only.
        var attendances = await context.Attendences
            .AsNoTracking()
            .Where(a =>
                a.EmployeeId == request.EmployeeId &&
                scheduleDates.Contains(a.Date))
            .ToListAsync(cancellationToken);

        var attendanceByDate = attendances
            .GroupBy(a => a.Date)
            .ToDictionary(g => g.Key, g => g.First());

        var responses = new List<GetEmployeeAttendenceResponse>();

        foreach (var schedule in schedules)
        {
            var shift = schedule.Shift;

            attendanceByDate.TryGetValue(schedule.Date, out var attendance);

            var checkIn = attendance?.CheckIn;
            var checkOut = attendance?.CheckOut;

            // Shift today that hasn't started yet: nothing to report.
            if (attendance is null &&
                schedule.Date == today &&
                currentTime < shift.StartTime)
            {
                continue;
            }

            // Only use "now" as the end time for today's open attendance.
            // For past days without check-out, we don't guess the hours.
            var isToday = schedule.Date == today;

            responses.Add(new GetEmployeeAttendenceResponse
            {
                Date = schedule.Date,
                CheckIn = checkIn,
                CheckOut = checkOut,
                LateCheckIn = CalculateLateCheckIn(
                    checkIn,
                    shift.StartTime,
                    shift.EndTime),
                WorkHours = CalculateWorkDuration(
                    checkIn,
                    checkOut,
                    isToday ? currentTime : null),
                Status = DetermineAttendanceStatus(checkIn, checkOut),
                StartBreak = shift.StartBreak,
                EndBreak = shift.EndBreak
            });
        }

        return Result<List<GetEmployeeAttendenceResponse>>
            .Success(responses);
    }

    private static string DetermineAttendanceStatus(
        TimeOnly? checkIn,
        TimeOnly? checkOut)
    {
        if (checkIn is null && checkOut is null)
            return AttendanceStatus.NoAttendance.ToString();

        if (checkIn is not null && checkOut is null)
            return AttendanceStatus.inComplete.ToString();

        return AttendanceStatus.completed.ToString();
    }

    private static TimeSpan CalculateLateCheckIn(
        TimeOnly? checkIn,
        TimeOnly shiftStart,
        TimeOnly shiftEnd)
    {
        if (checkIn is null)
            return TimeSpan.Zero;

        var actual = checkIn.Value;
        var isOvernight = shiftStart > shiftEnd;

        // Overnight shift (e.g. 22:00 -> 06:00) and the employee
        // checked in after midnight: that's late.
        if (isOvernight && actual <= shiftEnd)
        {
            return actual.ToTimeSpan()
                + TimeSpan.FromDays(1)
                - shiftStart.ToTimeSpan();
        }

        var late = actual - shiftStart;

        return late > TimeSpan.Zero ? late : TimeSpan.Zero;
    }

    private static TimeSpan CalculateWorkDuration(
        TimeOnly? checkIn,
        TimeOnly? checkOut,
        TimeOnly? currentTimeIfOngoing)
    {
        if (checkIn is null)
            return TimeSpan.Zero;

        var end = checkOut ?? currentTimeIfOngoing;

        // Open attendance on a past day: can't calculate reliably.
        if (end is null)
            return TimeSpan.Zero;

        var duration = end.Value - checkIn.Value;

        // Overnight shift.
        if (duration < TimeSpan.Zero)
            duration += TimeSpan.FromDays(1);

        return duration;
    }
}