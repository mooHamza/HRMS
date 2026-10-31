using HRMS.API.Features.Attendence.Commands;
using HRMS.API.Features.Attendence.Queries.GetEmployeeAttendence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Attendence
(
    IMediator _mediator
) : ControllerBase
{

    [HttpPost("ClockIn")]
    public async Task<IActionResult> ClockIn([FromBody] ClockInCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("ClockOut")]
    public async Task<IActionResult> ClockOut([FromBody] ClockOutCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Errors);
    }

    [HttpGet("GetEmployeeAttendence/{employeeId}")]
    public async Task<IActionResult> GetEmployeeAttendence(Guid employeeId)
    {
        var command = new GetEmployeeAttendenceQuery(employeeId);
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("Watch-Location")]
    public async Task<IActionResult> WatchLocation([FromBody] WatchLocationCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Errors);
    }
}