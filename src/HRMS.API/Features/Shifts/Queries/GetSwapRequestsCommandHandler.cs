

using HRMS.API.common;
using HRMS.API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Shifts.Queries;

public class GetSwapRequestsCommandHandler
(
    AppDbContext _context
) : IRequestHandler<GetSwapRequestsQuery, Result<List<SwapRequestResponse>>>
{
    

    public async Task<Result<List<SwapRequestResponse>>> Handle(GetSwapRequestsQuery request, CancellationToken cancellationToken)
    {
        var swapRequests = await _context.ShiftSwapRequests
            .Where(s => s.ToEmployeeSchedule.EmployeeId == request.EmployeeId)
            .Select(s => new SwapRequestResponse
            {
                ToEmployeeName = $"{s.ToEmployeeSchedule.Employee.FName} {s.ToEmployeeSchedule.Employee.SName}",
                FromDate = s.FromEmployeeSchedule.Date,
                ToDate = s.ToEmployeeSchedule.Date,
                FromSiteName = s.FromEmployeeSchedule.Site.Name,
                ToSiteName = s.ToEmployeeSchedule.Site.Name,
                FromShiftName = s.FromEmployeeSchedule.Shift.Name,
                ToShiftName = s.ToEmployeeSchedule.Shift.Name,
                FromStartTime = s.FromEmployeeSchedule.Shift.StartTime,
                ToStartTime = s.ToEmployeeSchedule.Shift.StartTime,
                FromStartBreak = s.FromEmployeeSchedule.Shift.StartBreak,
                ToEndBreak = s.ToEmployeeSchedule.Shift.EndBreak
            })
            .ToListAsync(cancellationToken);

        return Result<List<SwapRequestResponse>>.Success(swapRequests);
    }
}