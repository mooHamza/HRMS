namespace HRMS.API.Features.Schadule.Commands.SetSchaduleCommand;

using HRMS.API.common;
using MediatR;
public record SetSchaduleCommand(
    Guid EmployeeId,
    Guid ShiftId,
    Guid SiteId,
    DateOnly Date
) : IRequest<Result>;