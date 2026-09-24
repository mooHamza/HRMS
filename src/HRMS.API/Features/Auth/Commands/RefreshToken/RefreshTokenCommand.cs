using MediatR;
using HRMS.API.common;
using HRMS.API.Features.Auth.Commands.Login;

namespace HRMS.API.Features.Auth.Commands.RefreshToken;
public record RefreshTokenCommand(
    string RefreshToken
) : IRequest<Result<LoginResponse>>;