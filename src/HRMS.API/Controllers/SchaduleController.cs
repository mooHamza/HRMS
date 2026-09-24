using HRMS.API.Features.Schadule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HRMS.API.Features.Shifts.Queries;
using HRMS.API.Features.Shifts.Commands;

namespace HRMS.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class SchaduleController
(
    IMediator mediator
) : ControllerBase
{
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
        var query = new GetAllSchaduleQuery(employeeId);
        var result = await mediator.Send(query);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
    [HttpGet("swap-requests/{employeeId}")]
    public async Task<ActionResult<List<SwapRequestResponse>>> GetSwapRequests(Guid employeeId)
    {
        var query = new GetSwapRequestsQuery(employeeId);
        var result = await mediator.Send(query);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
    [HttpPost("swap-shift")]
    public async Task<ActionResult> SwapShift([FromBody] SwapShiftCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

}