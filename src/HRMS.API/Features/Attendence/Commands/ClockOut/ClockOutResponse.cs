namespace HRMS.API.Features.Attendence.Commands.ClockOut;

public record ClockOutResponse(
    Guid EmployeeId,
    DateOnly Date,
    TimeOnly CheckIn,
    TimeOnly CheckOut
);