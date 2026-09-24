using HRMS.API.Entities;
using HRMS.API.Features.Auth.DTOs;
using HRMS.API.common;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;

public record RegisterCommand(
    string FirstName,
     string LastName,
      string Email,
       string Password,
        Guid JobId,
        Guid? ManagerId,
        Guid DepartmentId,
        DateOnly HireDate
        ) : IRequest<Result<RegisterResponse>>;