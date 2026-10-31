namespace HRMS.API.Features.Attendence.Commands.ClockIn;

public record ClockInResponse(
    Guid EmployeeId,
    DateOnly Date,
    TimeOnly CheckIn,
    TimeOnly? CheckOut
);