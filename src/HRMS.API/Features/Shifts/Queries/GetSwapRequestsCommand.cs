

using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Shifts.Queries;

public record GetSwapRequestsQuery(Guid EmployeeId) : IRequest<Result<List<SwapRequestResponse>>>
{}

