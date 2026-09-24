using System.Security.Cryptography;
using HRMS.API.Entities;
using HRMS.API.common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HRMS.API.Data;

namespace HRMS.API.Features.Auth.Commands.VerifyResetOtp;

public class VerifyResetOtpCommandHandler(
    UserManager<Employee> userManager,
    AppDbContext dbContext,
    IPasswordHasher<Employee> passwordHasher
) : IRequestHandler<
        VerifyResetOtpCommand,
        Result<VerifyResetOtpResponse>>
{
    public async Task<Result<VerifyResetOtpResponse>> Handle(
        VerifyResetOtpCommand request,
        CancellationToken cancellationToken)
    {
        var employee =
            await userManager.FindByEmailAsync(request.Email);

        if (employee is null)
        {
            return Result<VerifyResetOtpResponse>.Failure(
                "Invalid OTP.");
        }

        var resetOtp = await dbContext.PasswordResetOtps
            .Where(x =>
                x.EmployeeId == employee.Id &&
                !x.IsUsed)
            .OrderByDescending(x => x.ExpiresAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (resetOtp is null)
        {
            return Result<VerifyResetOtpResponse>.Failure(
                "Invalid OTP.");
        }

        if (resetOtp.ExpiresAt <= DateTime.UtcNow)
        {
            return Result<VerifyResetOtpResponse>.Failure(
                "OTP has expired.");
        }

        if (resetOtp.FailedAttempts >= 5)
        {
            return Result<VerifyResetOtpResponse>.Failure(
                "Too many attempts.");
        }

        var verificationResult =
            passwordHasher.VerifyHashedPassword(
                employee,
                resetOtp.OtpHash,
                request.Otp);

        if (verificationResult ==
            PasswordVerificationResult.Failed)
        {
            resetOtp.FailedAttempts++;

            await dbContext.SaveChangesAsync(
                cancellationToken);

            return Result<VerifyResetOtpResponse>.Failure(
                "Invalid OTP.");
        }

        // OTP is valid
        resetOtp.IsUsed = true;

        // Generate short-lived reset session token
        var resetToken =
            Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

        var resetSession = new PasswordResetSession
        {
            ResetToken = resetToken,
            EmployeeId = employee.Id,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
            IsUsed = false
        };

        dbContext.PasswordResetSessions.Add(resetSession);

        await dbContext.SaveChangesAsync(
            cancellationToken);

        return Result<VerifyResetOtpResponse>.Success(
            new VerifyResetOtpResponse(resetToken)
        );
    }
}