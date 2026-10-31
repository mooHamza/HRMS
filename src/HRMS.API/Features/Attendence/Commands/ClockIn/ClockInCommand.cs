using HRMS.API.common;
using HRMS.API.Entities;
using HRMS.API.Features.Attendence.Commands.ClockIn;
using MediatR;

namespace HRMS.API.Features.Attendence.Commands;

public record ClockInCommand (
    Guid EmployeeId,
    double Latitude,
    double Longitude

): IRequest<Result<ClockInResponse>> ;
