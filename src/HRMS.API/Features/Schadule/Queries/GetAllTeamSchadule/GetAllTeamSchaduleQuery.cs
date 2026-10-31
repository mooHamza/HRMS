using HRMS.API.common;
using HRMS.API.Features.Schadule.Queries.GetAllTeamSchadule;
using MediatR;

namespace HRMS.API.Features.Schadule.Queries;

public record GetAllTeamScheduleQuery(Guid EmployeeId) : IRequest<Result<List<GetAllTeamScheduleResponse>>>
{
}