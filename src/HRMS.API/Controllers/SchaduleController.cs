using HRMS.API.Features.Schadule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HRMS.API.Features.Shifts.Queries;
using HRMS.API.Features.Shifts.Commands;
using HRMS.API.Features.Schadule.Commands.SetSchaduleCommand;
using HRMS.API.Features.Schadule.Shifts.Commands.AskSwapRequest;

namespace HRMS.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
// [Authorize]
public class SchaduleController
(
    IMediator mediator
) : ControllerBase
{
    [HttpPost("set-schadule")]
    public async Task<IActionResult> SetSchadule([FromBody] SetSchaduleCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
    
    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<List<GetEmployeeSchaduleResponse>>> GetEmployeeSchadule( Guid employeeId)
    {
        var query = new GetEmployeeSchaduleQuery(employeeId);
        var result = await mediator.Send(query);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
    
    [HttpGet("all/{employeeId}")]
    public async Task<ActionResult<List<GetEmployeeSchaduleResponse>>> GetAllEmployeeSchadules(Guid employeeId)
    {
        var query = new GetAllTeamScheduleQuery(employeeId);
        var result = await mediator.Send(query);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

}