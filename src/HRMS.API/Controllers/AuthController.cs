using HRMS.API.Features.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.API.Features.Auth.DTOs;
using HRMS.API.Features.Auth.Commands.ForgetPassword;
using HRMS.API.Features.Auth.Commands.VerifyResetOtp;
using HRMS.API.Features.Auth.ResetPassword;
using Microsoft.AspNetCore.Authorization;
using HRMS.API.Features.Auth.Commands.ChangePassword;
using HRMS.API.Features.Auth.Commands.RefreshToken;
namespace HRMS.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : Unauthorized(result.Errors);
    }
    [HttpPost("refresh-token")]
    public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : Unauthorized(result.Errors);
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
    [HttpPost("verify-reset-otp")]
    public async Task<IActionResult> VerifyResetOtp([FromBody] VerifyResetOtpCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
[Authorize]
[HttpPost("change-password")]
public async Task<IActionResult> ChangePassword(
    ChangePasswordCommand command)
{
    var result = await _mediator.Send(command);

    if (!result.IsSuccess)
        return BadRequest(result.Errors);

    return Ok();
}
}