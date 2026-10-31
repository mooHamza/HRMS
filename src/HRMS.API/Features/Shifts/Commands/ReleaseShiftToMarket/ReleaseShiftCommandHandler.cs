using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Shifts.Commands.ReleaseShiftToMarket;

public class ReleaseShiftCommandHandler(AppDbContext _context)
    : IRequestHandler<ReleaseShiftCommand, Result>
{
    public async Task<Result> Handle(
        ReleaseShiftCommand request,
        CancellationToken cancellationToken)
    {
        var shift = await _context.EmployeeSchedules
            .FindAsync(request.ScheduleId, cancellationToken);

        if (shift == null)
        {
            return Result.Failure("Shift not found.");
        }
        
        var IsAlreadyReleased = await _context.OpenShifts
            .AnyAsync(os => os.ScheduleId == request.ScheduleId, cancellationToken);

        if (IsAlreadyReleased)
        {
            return Result.Failure("Shift is already released.");
        }

        // Create a new OpenShift entity
        var openShift = new OpenShift
        {
            ScheduleId = request.ScheduleId,
            ShiftId = request.ShiftId,
            SiteId = request.SiteId,
            Date = request.Date,
            ReleasedByEmployeeId = request.ReleasedByEmployeeId,
            ClaimedByEmployeeId = request.ClaimedByEmployeeId,
            JobId = request.JobId
        };

        // Add the OpenShift to the context
        _context.OpenShifts.Add(openShift);

        // Save changes to the database
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
        
    }
}