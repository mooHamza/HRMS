namespace HRMS.API.Features.Auth.Commands.Login;

public record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt,
    DateTime RefreshTokenExpiresAt,
    bool MustChangePassword

);