using HRMS.API.common;
using HRMS.API.Entities;
using HRMS.API.Features.Attendence.Commands.ClockOut;
using MediatR;

namespace HRMS.API.Features.Attendence.Commands;

public record ClockOutCommand (
    Guid EmployeeId
): IRequest<Result<ClockOutResponse>> ;