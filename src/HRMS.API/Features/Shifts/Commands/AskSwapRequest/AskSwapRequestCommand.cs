using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Schadule.Shifts.Commands.AskSwapRequest;

public record AskSwapRequestCommand(
    Guid FromShiftScheduleId,
    Guid ToShiftScheduleId
) : IRequest<Result>;