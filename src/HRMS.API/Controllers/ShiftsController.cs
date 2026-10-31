using HRMS.API.Features.Schadule.Shifts.Commands.AskSwapRequest;
using HRMS.API.Features.Shifts.Commands;
using HRMS.API.Features.Shifts.Commands.ClaimShiftFromMarket;
using HRMS.API.Features.Shifts.Commands.ReleaseShiftToMarket;
using HRMS.API.Features.Shifts.Queries;
using HRMS.API.Features.Shifts.Queries.GetMarketShifts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers;


[ApiController]
[Route("api/[controller]")]

public class ShiftsController
(
    IMediator _mediator
) : ControllerBase
{
   
    [HttpGet("swap-requests/{employeeId}")]
    public async Task<ActionResult<List<SwapRequestResponse>>> GetSwapRequests(Guid employeeId)
    {
        var query = new GetSwapRequestsQuery(employeeId);
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
    [HttpPost("accept-swap-shift")]
    public async Task<ActionResult> SwapShift([FromBody] SwapShiftCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
    [HttpPost("ask-swap-request")]  
    public async Task<ActionResult> AskSwapRequest([FromBody] AskSwapRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Errors);
        }

    [HttpPost("release-shift")]
    public async Task<ActionResult> SendShiftToMarket([FromBody] ReleaseShiftCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
   
   [HttpGet("market-shifts/{employeeId}")]
    public async Task<ActionResult<List<GetMarketShiftsResponse>>> GetMarketShifts(Guid employeeId)
    {
        var query = new GetMarketShiftsQuery(employeeId);
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpPost("claim-shift")]
    public async Task<ActionResult> ClaimShift([FromBody] ClaimShiftCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

}