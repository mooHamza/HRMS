using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Auth.ResetPassword;
public class ResetPasswordCommandHandler(
    AppDbContext dbContext,
    UserManager<Employee> userManager
) : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var session = await dbContext.PasswordResetSessions
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(
                x => x.ResetToken == request.ResetToken,
                cancellationToken);

        if (session is null)
        {
            return Result.Failure(
                "Invalid reset token.");
        }

        if (session.IsUsed)
        {
            return Result.Failure(
                "Reset token has already been used.");
        }

        if (session.ExpiresAt <= DateTime.UtcNow)
        {
            return Result.Failure(
                "Reset token has expired.");
        }

        var result = await userManager.ResetPasswordAsync(
            session.Employee,
            await userManager.GeneratePasswordResetTokenAsync(
                session.Employee),
            request.NewPassword);

        if (!result.Succeeded)
        {
            return Result.Failure(
                result.Errors
                    .Select(x => x.Description)
                    .ToArray());
        }

        session.IsUsed = true;

        await dbContext.SaveChangesAsync(
            cancellationToken);

        return Result.Success();
    }
}