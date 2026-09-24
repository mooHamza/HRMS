using HRMS.API.Entities;
using HRMS.API.Features.Departments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DepartmentsController(
IMediator _mediator
) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<DepartmentResponse>>> GetAllDepartments()
    {
        var result = await _mediator.Send(new GetAllDepartmentsQuery());
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
