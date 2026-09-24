using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Auth.ResetPassword;

public record ResetPasswordCommand(
    string Email,
    string NewPassword,
    string ResetToken
) : IRequest<Result>;