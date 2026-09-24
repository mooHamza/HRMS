
using HRMS.API.common;
using HRMS.API.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
namespace HRMS.API.Features.Auth.Commands.ChangePassword;

public class ChangePasswordCommandHandler
(
UserManager<Employee> _userManager,
IHttpContextAccessor _httpContextAccessor
) : IRequestHandler<ChangePasswordCommand, Result>
{
    public async Task<Result> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor
            .HttpContext?
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Result.Failure("Unauthorized");

        var employee = await _userManager.FindByIdAsync(userId);

        if (employee is null)
            return Result.Failure("User not found");

        var result = await _userManager.ChangePasswordAsync(
            employee,
            request.CurrentPassword,
            request.NewPassword);

        if (!result.Succeeded)
        {
            return Result.Failure(
                result.Errors.First().Description);
        }
        employee.MustChangePassword = false;

        await _userManager.UpdateAsync(employee);

        return Result.Success();
    }
}