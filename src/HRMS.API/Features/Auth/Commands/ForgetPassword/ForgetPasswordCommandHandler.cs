using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.common;
using HRMS.API.Features.Auth.Services;
using MediatR;  
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
namespace HRMS.API.Features.Auth.Commands.ForgetPassword;
public class ForgotPasswordCommandHandler(
    UserManager<Employee> userManager,
    AppDbContext dbContext,
    IPasswordHasher<Employee> passwordHasher,
    IEmailService emailService
) : IRequestHandler<ForgetPasswordCommand, Result>
{
    public async Task<Result> Handle(
          ForgetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var employee =
            await userManager.FindByEmailAsync(request.Email);

        // Don't reveal whether the email exists
        if (employee is null)
            return Result.Success();

        var otp = RandomNumberGenerator
            .GetInt32(100000, 1000000)
            .ToString();

        var otpHash = passwordHasher.HashPassword(
            employee,
            otp);

        var resetOtp = new PasswordResetOtp
        {
            EmployeeId = employee.Id,
            OtpHash = otpHash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(1),
            IsUsed = false,
            FailedAttempts = 0
        };

        dbContext.PasswordResetOtps.Add(resetOtp);

        await dbContext.SaveChangesAsync(cancellationToken);

        await emailService.SendEmailAsync(
            employee.Email!,
            "HRMS Password Reset Code",
            $"""
            <h2>Password Reset</h2>
            <p>Your password reset code is:</p>
            <h1>{otp}</h1>
            <p>This code expires in 1 minute.</p>
            """
        );

        return Result.Success();
    }
}