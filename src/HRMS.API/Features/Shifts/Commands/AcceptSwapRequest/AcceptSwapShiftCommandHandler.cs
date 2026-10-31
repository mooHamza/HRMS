

using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Shifts.Commands;
public class SwapShiftCommandHandler

(
    AppDbContext _context
) : IRequestHandler<SwapShiftCommand, Result>
{
    

    public async Task<Result> Handle(SwapShiftCommand request, CancellationToken cancellationToken)
    {
         
        var fromSchedule = await _context.EmployeeSchedules.FirstOrDefaultAsync(s => s.Id == request.FromEmployeeScheduleId, cancellationToken);
        var toSchedule = await _context.EmployeeSchedules.FirstOrDefaultAsync(s => s.Id == request.ToEmployeeScheduleId, cancellationToken);

        if (fromSchedule == null || toSchedule == null)
        {
            return Result.Failure("One or both schedules not found.");
        }

        var swapRequest = await _context.SwapRequests.FirstOrDefaultAsync(sr => sr.FromEmployeeScheduleId == request.FromEmployeeScheduleId && sr.ToEmployeeScheduleId == request.ToEmployeeScheduleId, cancellationToken);
        
        if (swapRequest == null)
        {
            return Result.Failure("Swap request not found.");
        }

        if (swapRequest.Status != ShiftStatus.PendingSwap)
        {
            return Result.Failure("Swap request is not pending or already swapped.");
        }


        // swap 
      (fromSchedule.EmployeeId, toSchedule.EmployeeId) =
        (toSchedule.EmployeeId, fromSchedule.EmployeeId);


        swapRequest.Status = ShiftStatus.swapped;
    
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}