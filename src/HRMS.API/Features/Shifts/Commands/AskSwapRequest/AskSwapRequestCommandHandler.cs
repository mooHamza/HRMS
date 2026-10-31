using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.Features.Schadule.Shifts.Commands.AskSwapRequest;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace HRMS.API.Features.Shifts.Commands.AskSwapRequest;
public class AskSwapRequestCommandHandler
(
    AppDbContext _context
) : IRequestHandler<AskSwapRequestCommand, Result>
{


    public async Task<Result> Handle(AskSwapRequestCommand request, CancellationToken cancellationToken)
    {
        var fromShiftSchedule = await _context.EmployeeSchedules.FirstOrDefaultAsync(es => es.Id == request.FromShiftScheduleId, cancellationToken);
        var toShiftSchedule = await _context.EmployeeSchedules.FirstOrDefaultAsync(es => es.Id == request.ToShiftScheduleId, cancellationToken);

        if (fromShiftSchedule == null || toShiftSchedule == null)
        {
            return Result.Failure("One or both shift schedules not found.");
        }

        var IsExistingRequest = await _context.SwapRequests.AnyAsync(sr => sr.FromEmployeeScheduleId == request.FromShiftScheduleId && sr.ToEmployeeScheduleId == request.ToShiftScheduleId, cancellationToken);
        if (IsExistingRequest)
        {
            return Result.Failure("A swap request for these shift schedules already exists.");
        }
        var shiiftSwapRequests = new SwapRequest
        {
            FromEmployeeScheduleId = request.FromShiftScheduleId,
            ToEmployeeScheduleId = request.ToShiftScheduleId         
        };
    
        await _context.SwapRequests.AddAsync(shiiftSwapRequests);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}