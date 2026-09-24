using HRMS.API.common;
using HRMS.API.Entities;
using MediatR;

namespace HRMS.API.Features.Departments.Queries;

public class GetAllDepartmentsQueryHandler
(
    BaseRepository<Department> _context
) : IRequestHandler<GetAllDepartmentsQuery, Result<List<DepartmentResponse>>>
{
  

    public async Task<Result<List<DepartmentResponse>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _context.GetAllAsync();
        var departmentResponses = departments.Select(d => new DepartmentResponse(d.Id, d.Name)).ToList();
        return Result<List<DepartmentResponse>>.Success(departmentResponses);
    }
}