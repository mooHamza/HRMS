namespace HRMS.API.Features.Auth.DTOs;
public record RegisterResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);