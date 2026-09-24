using HRMS.API.common;
using HRMS.API.Entities;
using HRMS.API.Features.Auth.Commands.Login;
using HRMS.API.Features.Auth.DTOs;
using HRMS.API.Features.Auth.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HRMS.API.Features.Auth.Commands.Login;

public class LoginCommandHandler(
    UserManager<Employee> userManager,
    ITokenService tokenService
) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var employee =
            await userManager.FindByEmailAsync(request.Email);

        if (employee is null)
        {
            return Result<LoginResponse>.Failure(
                "Invalid email or password.");
        }

        var passwordValid =
            await userManager.CheckPasswordAsync(
                employee,
                request.Password);

        if (!passwordValid)
        {
            return Result<LoginResponse>.Failure(
                "Invalid email or password.");
        }

        var tokens =
            await tokenService.GenerateAccessTokensAsync(employee);

        return Result<LoginResponse>.Success(tokens);
    }
}