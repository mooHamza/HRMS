using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Shifts.Queries.GetMarketShifts;

public record GetMarketShiftsQuery(
    Guid EmployeeId
    ) : IRequest<Result<List<GetMarketShiftsResponse>>>;
