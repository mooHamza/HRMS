using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Auth.Commands.ChangePassword;

public record ChangePasswordCommand(
    string CurrentPassword,
    string NewPassword
) : IRequest<Result>;