using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Auth.Commands.VerifyResetOtp;
public record VerifyResetOtpCommand(
    string Email,
    string Otp
) : IRequest<Result<VerifyResetOtpResponse>>;