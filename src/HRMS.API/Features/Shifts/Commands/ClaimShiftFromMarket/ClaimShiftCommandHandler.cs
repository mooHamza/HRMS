

using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Shifts.Commands.ClaimShiftFromMarket;

public class ClaimShiftCommandHandler(AppDbContext _context,UserManager<Employee> _userManager)
    : IRequestHandler<ClaimShiftCommand, Result>
{
    public async Task<Result> Handle(
        ClaimShiftCommand request, CancellationToken ct)
    {
        var openShift = await _context.OpenShifts
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, ct);

        if (openShift is null )
            return Result.Failure("Shift is no longer available.");

        var employee = await _userManager.FindByIdAsync(request.EmployeeId.ToString());
        if (employee is null)
            return Result.Failure("Employee not found.");

        if (employee.JobId != openShift.JobId)
            return Result.Failure("This shift is not for your position.");

        var busy = await _context.EmployeeSchedules.AnyAsync(s =>
            s.EmployeeId == request.EmployeeId && s.Date == openShift.Date, ct);
        if (busy)
            return Result.Failure("You already have a shift on this date.");

        var original = await _context.EmployeeSchedules
            .FirstOrDefaultAsync(s => s.Id == request.ScheduleId, ct);
        if (original is null)
            return Result.Failure("Schedule not found.");

        // 1) Remove the original employee's schedule 
        _context.EmployeeSchedules.Remove(original);

        // 2) Create the claimer's schedule 
        _context.EmployeeSchedules.Add(new EmployeeSchedule
        {
            EmployeeId = request.EmployeeId,
            ShiftId = original.ShiftId,
            SiteId = original.SiteId,
            Date = original.Date
        });

        // 3) Close the open shift
        openShift.ClaimedByEmployeeId = request.EmployeeId;
        openShift.Status = ShiftStatus.claimed;

        try
        {
            await _context.SaveChangesAsync(ct);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failure("Someone else claimed this shift first.");
        }

        return Result.Success();
    }

}