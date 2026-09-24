using HRMS.API.common;
using HRMS.API.Entities;
using MediatR;

namespace HRMS.API.Features.Departments.Queries;

public record GetAllDepartmentsQuery() : IRequest<Result<List<DepartmentResponse>>>;