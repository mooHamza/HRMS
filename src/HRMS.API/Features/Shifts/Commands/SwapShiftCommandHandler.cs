

using HRMS.API.common;
using HRMS.API.Data;
using MediatR;

namespace HRMS.API.Features.Shifts.Commands;
public class SwapShiftCommandHandler

(
    AppDbContext _context
) : IRequestHandler<SwapShiftCommand, Result>
{
    

    public async Task<Result> Handle(SwapShiftCommand request, CancellationToken cancellationToken)
    {
        var fromSchedule = await _context.EmployeeSchedules.FindAsync(request.FromEmployeeScheduleId);
        var toSchedule = await _context.EmployeeSchedules.FindAsync(request.ToEmployeeScheduleId);

        if (fromSchedule == null || toSchedule == null)
        {
            return Result.Failure("One or both schedules not found.");
        }

        // Swap the employee IDs
        var tempEmployeeId = fromSchedule.EmployeeId;
        fromSchedule.EmployeeId = toSchedule.EmployeeId;
        toSchedule.EmployeeId = tempEmployeeId;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}