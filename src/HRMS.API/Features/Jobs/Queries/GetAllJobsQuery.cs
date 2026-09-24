using HRMS.API.common;
using HRMS.API.Entities;
using MediatR;

namespace HRMS.API.Features.Jobs.Queries;

public record GetAllJobsQuery() : IRequest<Result<List<GetJobResponse>>>;