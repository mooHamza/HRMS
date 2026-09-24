using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.Features.Auth.Commands.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HRMS.API.Features.Auth.Services;

public class TokenService(
    IConfiguration configuration,
    UserManager<Employee> userManager,
    AppDbContext dbContext
) : ITokenService
{
    public async Task<LoginResponse> GenerateAccessTokensAsync(
        Employee employee)
    {
        var roles = await userManager.GetRolesAsync(employee);

        var claims = new List<Claim>
        {
               new Claim(
        ClaimTypes.NameIdentifier,
        employee.Id.ToString()),

    new Claim(
        ClaimTypes.Email,
        employee.Email!),
    new Claim(
        "Job",
        employee!.Job?.Title ?? string.Empty),

    new Claim(
        ClaimTypes.Name,
        $"{employee.FName} {employee.SName}")
        };

        foreach (var role in roles)
        {
            claims.Add(
                new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var accessTokenExpiresAt =
            DateTime.UtcNow.AddMinutes(
                int.Parse(
                    configuration["Jwt:AccessTokenExpirationMinutes"]!));

        var jwt = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: accessTokenExpiresAt,
            signingCredentials: credentials
        );

        var accessToken =
            new JwtSecurityTokenHandler()
                .WriteToken(jwt);

        // Generate refresh token
        var refreshToken = GenerateRefreshToken();

        var refreshTokenExpiresAt =
            DateTime.UtcNow.AddDays(
                int.Parse(
                    configuration["Jwt:RefreshTokenExpirationDays"]!));

        dbContext.RefreshTokens.Add(
            new RefreshToken
            {
                Id = Guid.NewGuid(),
                    Token = refreshToken,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = refreshTokenExpiresAt,
                    IsRevoked = false,      
                    EmployeeId = employee.Id
            });

        await dbContext.SaveChangesAsync();

        return new LoginResponse(
            accessToken,
            refreshToken,
            accessTokenExpiresAt,
            refreshTokenExpiresAt,
            employee.MustChangePassword
        );
    }

    private static string GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(bytes);
    }
}