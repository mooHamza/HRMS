using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Schadule.Queries;
public record GetEmployeeSchaduleQuery(Guid EmployeeId) : IRequest<Result<List<GetEmployeeSchaduleResponse>>>
{
}