using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.Features.Attendence.Commands.ClockIn;
using HRMS.API.Features.Attendence.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Attendence.Commands;

public class ClockInCommandHandler
(
    AppDbContext _context
) : IRequestHandler<ClockInCommand, Result<ClockInResponse>>
{
  

    public async Task<Result<ClockInResponse>> Handle(ClockInCommand request, CancellationToken cancellationToken)
    {

        var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
           "Egypt Standard Time");

        var nowUtc = DateTime.UtcNow;

        var nowInEgypt = TimeZoneInfo.ConvertTimeFromUtc(
            nowUtc,
            egyptTimeZone);

        var todayInEgypt = DateOnly.FromDateTime(nowInEgypt);
        var currentTime = TimeOnly.FromDateTime(nowInEgypt);

        var schedule = await _context.EmployeeSchedules
        .Include(s => s.Shift)
        .Include(s => s.Site)
            .FirstOrDefaultAsync(s => s.EmployeeId == request.EmployeeId && s.Date == todayInEgypt, cancellationToken);
        
        if (schedule == null)
        {
            return Result<ClockInResponse>.Failure("No schedule found for the given employee and date.");
        }

    


        var startTime = schedule.Shift.StartTime;
        var endTime = schedule.Shift.EndTime;

        var isOvernightShift = startTime > endTime;

        var isOnShiftTime = isOvernightShift
            ? currentTime >= startTime || currentTime <= endTime
            : currentTime >= startTime && currentTime <= endTime;

        if (!isOnShiftTime)
        {
            return Result<ClockInResponse>.Failure(
                "You are not within your scheduled shift time.");
        }

        var distance = CalculateAllowedDistance.CalculateDistanceInMeters(schedule.Site.Latitude, schedule.Site.Longitude, request.Latitude, request.Longitude);

        if (distance > AttendenceConstants.AllowedDistanceInMeters)
        {
            return Result<ClockInResponse>.Failure("You are not within the allowed distance to clock in.");
        }

        var attendance = new Attendance
        {
            EmployeeId = request.EmployeeId,
            Date = todayInEgypt,
            CheckIn = currentTime
        };

       await _context.Attendences.AddAsync(attendance, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = new ClockInResponse(
            EmployeeId: attendance.EmployeeId,
            Date: attendance.Date,
            CheckIn: attendance.CheckIn.Value,
            CheckOut: attendance.CheckOut
        );
        return Result<ClockInResponse>.Success(response);
    }
}