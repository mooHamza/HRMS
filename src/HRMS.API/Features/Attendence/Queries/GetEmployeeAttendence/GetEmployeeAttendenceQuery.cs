using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Attendence.Queries.GetEmployeeAttendence;

public record GetEmployeeAttendenceQuery(
    Guid EmployeeId
) : IRequest<Result<List<GetEmployeeAttendenceResponse>>>;
