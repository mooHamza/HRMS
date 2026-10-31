using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Attendence.Commands;

public record WatchLocationCommand(
    Guid EmployeeId,
    double Latitude,
    double Longitude
) : IRequest<Result<string>>;