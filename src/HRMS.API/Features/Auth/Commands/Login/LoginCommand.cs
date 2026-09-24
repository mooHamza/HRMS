using HRMS.API.Features.Auth.DTOs;
using HRMS.API.common;
using MediatR;
namespace HRMS.API.Features.Auth.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<Result<LoginResponse>>;