using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Shifts.Commands.ClaimShiftFromMarket;

public record ClaimShiftCommand(
    Guid EmployeeId,
    Guid ScheduleId
) : IRequest<Result>;
