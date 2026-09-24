using HRMS.API.common;
using MediatR;

namespace HRMS.API.Features.Auth.Commands.ForgetPassword;
public record ForgetPasswordCommand( string Email) : IRequest<Result>;
