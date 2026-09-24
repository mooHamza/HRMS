using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Schadule.Queries;

public record GetAllSchaduleQuery(Guid EmployeeId) : IRequest<Result<List<GetEmployeeSchaduleResponse>>>
{
}