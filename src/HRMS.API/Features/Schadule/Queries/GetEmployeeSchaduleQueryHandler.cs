using HRMS.API.common;
using HRMS.API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Schadule.Queries;

public class GetEmployeeSchaduleQueryHandler(
    AppDbContext _context
) : IRequestHandler<
    GetEmployeeSchaduleQuery,
    Result<List<GetEmployeeSchaduleResponse>>>
{
    public async Task<Result<List<GetEmployeeSchaduleResponse>>> Handle(
        GetEmployeeSchaduleQuery request,
        CancellationToken cancellationToken)
    {
        var employeeSchedule = await _context.EmployeeSchedules
            .Where(es => es.EmployeeId == request.EmployeeId)
            .Select(es => new GetEmployeeSchaduleResponse(
                request.EmployeeId,
                es.Employee!.Job!.Title ?? string.Empty,
                es.Date,
                es.Site!.Name ?? string.Empty,
                es.Shift!.Name ?? string.Empty,
                es.Shift.StartTime,
                es.Shift.EndTime,
                es.Shift.StartBreak,
                es.Shift.EndBreak
            ))
            .ToListAsync(cancellationToken);

        if (employeeSchedule.Count == 0)
        {
            return Result<List<GetEmployeeSchaduleResponse>>.Failure(
                $"There is no schedule found for the employee with Id {request.EmployeeId}."
            );
        }

        return Result<List<GetEmployeeSchaduleResponse>>.Success(employeeSchedule);
    }
}