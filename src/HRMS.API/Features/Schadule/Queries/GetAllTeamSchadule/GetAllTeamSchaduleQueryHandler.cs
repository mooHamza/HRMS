using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using HRMS.API.Features.Schadule.Queries.GetAllTeamSchadule;

namespace HRMS.API.Features.Schadule.Queries;
public class GetAllSchaduleQueryHandler
(
    AppDbContext _context,
    UserManager<Employee> _userManager
) : IRequestHandler<GetAllTeamScheduleQuery, Result<List<GetAllTeamScheduleResponse>>>
{
  

    public async Task<Result<List<GetAllTeamScheduleResponse>>> Handle(GetAllTeamScheduleQuery request, CancellationToken cancellationToken)
    {

        var empJob = await _userManager.Users
            .Where(e => e.Id == request.EmployeeId)
            .Select(e => e.JobId)
            .FirstOrDefaultAsync(cancellationToken);



        var employeeSchedules = await _context.EmployeeSchedules
                        .Where(es=>es.Employee!.JobId == empJob && es.EmployeeId != request.EmployeeId).Select(es=>new GetAllTeamScheduleResponse(
                            es.Id,
                            es.EmployeeId,
                            $"{es.Employee!.FName} {es.Employee!.SName}",
                            es.Employee!.Job!.Title ?? string.Empty,
                            es.Date,
                            es.Shift!.Name ?? string.Empty,
                            es.Site!.Name ?? string.Empty,
                            es.Shift.StartTime,
                            es.Shift.EndTime,
                            es.Shift.StartBreak,
                            es.Shift.EndBreak
                        )).ToListAsync(cancellationToken);

        return Result<List<GetAllTeamScheduleResponse>>.Success(employeeSchedules);
    }
}


