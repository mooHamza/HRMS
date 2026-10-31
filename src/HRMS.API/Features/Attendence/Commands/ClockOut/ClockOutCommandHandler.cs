using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.Features.Attendence.Commands.ClockOut;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Attendence.Commands;

public class ClockOutCommandHandler
(
    AppDbContext _context
) : IRequestHandler<ClockOutCommand, Result<ClockOutResponse>>
{
    
    public async Task<Result<ClockOutResponse>> Handle(ClockOutCommand request, CancellationToken cancellationToken)
    {
        var egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
            "Egypt Standard Time");

        var nowUtc = DateTime.UtcNow;

        var nowInEgypt = TimeZoneInfo.ConvertTimeFromUtc(
            nowUtc,
            egyptTimeZone);

        var todayInEgypt = DateOnly.FromDateTime(nowInEgypt);
        var currentTime = TimeOnly.FromDateTime(nowInEgypt);

        var attendance = await _context.Attendences
            .FirstOrDefaultAsync(a => a.EmployeeId == request.EmployeeId && a.Date == todayInEgypt, cancellationToken);

        if (attendance == null)
        {
            return Result<ClockOutResponse>.Failure("Attendance record not found for the given employee and date.");
        }
        if (attendance.CheckOut != null)
        {
            return Result<ClockOutResponse>.Failure("You have already clocked out for today.");
        }
        

        

        attendance.CheckOut = currentTime;
        await _context.SaveChangesAsync(cancellationToken);

        var response = new ClockOutResponse(
            EmployeeId: attendance.EmployeeId,
            Date: attendance.Date,
            CheckIn: attendance.CheckIn ?? TimeOnly.MinValue,
            CheckOut: attendance.CheckOut ?? TimeOnly.MinValue
        );

        return Result<ClockOutResponse>.Success(response);
    }
}