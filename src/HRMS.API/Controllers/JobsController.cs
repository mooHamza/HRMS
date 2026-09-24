using HRMS.API.Data;
using HRMS.API.Entities;
using HRMS.API.Features.Jobs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class JobsController
(
   IMediator _Mediator
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GetJobResponse>>> GetAllJobs()
    {
        var result = await _Mediator.Send(new GetAllJobsQuery());
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}