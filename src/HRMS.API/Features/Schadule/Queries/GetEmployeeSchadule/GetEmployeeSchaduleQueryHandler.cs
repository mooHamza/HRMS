
using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Schadule.Queries;

public class GetEmployeeSchaduleQueryHandler(
    AppDbContext context
) : IRequestHandler<
    GetEmployeeSchaduleQuery,
    Result<List<GetEmployeeSchaduleResponse>>>
{
    public async Task<Result<List<GetEmployeeSchaduleResponse>>> Handle(
        GetEmployeeSchaduleQuery request,
        CancellationToken cancellationToken)
    {
        var schedules = await context.EmployeeSchedules
            .AsNoTracking()
            .Where(es => es.EmployeeId == request.EmployeeId)
            .OrderBy(es => es.Date)
            .Select(es => new GetEmployeeSchaduleResponse(
                es.Id,
                es.EmployeeId,
                $"{es.Employee!.FName} {es.Employee!.SName}",
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

        if (schedules.Count == 0)
        {
            return Result<List<GetEmployeeSchaduleResponse>>.Failure(
                $"There is no schedule found for employee {request.EmployeeId}."
            );
        }

        return Result<List<GetEmployeeSchaduleResponse>>.Success(schedules);
    }
}
