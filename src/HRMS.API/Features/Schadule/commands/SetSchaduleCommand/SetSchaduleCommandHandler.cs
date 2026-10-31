using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.Features.Schadule.Commands.SetSchaduleCommand;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class SetSchaduleCommandHandler
(
    AppDbContext _context,
    UserManager<Employee> _userManager
) : IRequestHandler<SetSchaduleCommand, Result>
{
   

    public async Task<Result> Handle(SetSchaduleCommand request, CancellationToken cancellationToken)
    {
        var employee = await _userManager.FindByIdAsync(request.EmployeeId.ToString());
        if (employee == null)
            return Result.Failure("Employee not found");

        var shift = await _context.Shifts.FindAsync(request.ShiftId);
        if (shift == null)
            return Result.Failure("Shift not found");

        var site = await _context.Sites.FindAsync(request.SiteId);
        if (site == null)
            return Result.Failure("Site not found");

        var schedule = new EmployeeSchedule
        {
            EmployeeId = request.EmployeeId,
            ShiftId = request.ShiftId,
            SiteId = request.SiteId,
            Date = request.Date
        };

        await _context.EmployeeSchedules.AddAsync(schedule);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}