using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Shifts.Queries.GetMarketShifts;

public class GetMarketShiftsHandler
(AppDbContext _context,
UserManager<Employee> _userManager
)
    : IRequestHandler<GetMarketShiftsQuery, Result<List<GetMarketShiftsResponse>>>
{
    public async Task<Result<List<GetMarketShiftsResponse>>> Handle(
        GetMarketShiftsQuery request,
        CancellationToken cancellationToken)
    {
        var employee = await _userManager.FindByIdAsync(request.EmployeeId.ToString());

        if (employee == null)
        {
            return Result<List<GetMarketShiftsResponse>>.Failure("Employee not found.");
        }

        // Fetch the market shifts for the employee
        var marketShifts = await _context.OpenShifts
            .Where(os => os.ClaimedByEmployeeId == null && os.Status == Enums.ShiftStatus.Open && os.JobId == employee.JobId)
            .Select(os=>os.ScheduleId)
            .ToListAsync(cancellationToken);

        var marketShiftsResponse = await _context.EmployeeSchedules
            .Where(s => marketShifts.Contains(s.Id))
            .Include(s => s.Shift)
            .Include(s => s.Site)
            .Select(s => new GetMarketShiftsResponse
            {
                ScheduleId = s.Id,
                Date = s.Date,
                ShiftName = s.Shift.Name,
                SiteName = s.Site.Name,
                StartTime = s.Shift.StartTime,
                EndTime = s.Shift.EndTime,
                StartBreak = s.Shift.StartBreak,
                EndBreak = s.Shift.EndBreak,    
                ReleasedByEmployeeId = s.EmployeeId,
                JobId = s.Employee.JobId            
            })
            .ToListAsync(cancellationToken);

        // Map the market shifts to a response model
     

        return Result<List<GetMarketShiftsResponse>>.Success(marketShiftsResponse);
        
    }
}