using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Shifts.Commands;
public record SwapShiftCommand(Guid FromEmployeeScheduleId, Guid ToEmployeeScheduleId) : IRequest<Result>
{}
