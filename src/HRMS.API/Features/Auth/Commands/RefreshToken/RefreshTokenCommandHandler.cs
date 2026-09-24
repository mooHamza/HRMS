using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.common;
using HRMS.API.Features.Auth.Commands.Login;
using HRMS.API.Features.Auth.Commands.RefreshToken;
using HRMS.API.Features.Auth.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class RefreshTokenCommandHandler(
    AppDbContext dbContext,
    ITokenService tokenService
) : IRequestHandler<
        RefreshTokenCommand,
        Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var storedToken = await dbContext.RefreshTokens
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(
                x => x.Token == request.RefreshToken,
                cancellationToken);

        if (storedToken is null)
        {
            return Result<LoginResponse>.Failure(
                "Invalid refresh token.");
        }

        if (storedToken.IsRevoked)
        {
            return Result<LoginResponse>.Failure(
                "Refresh token has been revoked.");
        }

        if (storedToken.ExpiresAt <= DateTime.UtcNow)
        {
            return Result<LoginResponse>.Failure(
                "Refresh token has expired.");
        }

        var employee = storedToken.Employee;

        // Generate new access token
        var tokenResponse =
            await tokenService.GenerateAccessTokensAsync(employee);


        // Revoke old token
        storedToken.IsRevoked = true;

        // Store new token
        var newRefreshTokenEntity = new RefreshToken
        {
            Token = tokenResponse.RefreshToken,
            EmployeeId = employee.Id,
            ExpiresAt = tokenResponse.RefreshTokenExpiresAt,
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false
        };

        dbContext.RefreshTokens.Add(newRefreshTokenEntity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<LoginResponse>.Success(
            new LoginResponse(
                tokenResponse.AccessToken,
                tokenResponse.RefreshToken,
                tokenResponse.AccessTokenExpiresAt,
                tokenResponse.RefreshTokenExpiresAt,
                employee.MustChangePassword 
            ));
    }
}