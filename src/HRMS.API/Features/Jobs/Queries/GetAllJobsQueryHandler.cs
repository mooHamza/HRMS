using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace HRMS.API.Features.Jobs.Queries;

public class GetAllJobsQueryHandler
(
    BaseRepository<Job> _Context
) : IRequestHandler<GetAllJobsQuery, Result<List<GetJobResponse>>>
{
  

    public async Task<Result<List<GetJobResponse>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
    {
        var jobs = await _Context.GetAllAsync();
        var jobResponses = jobs.Select(j => new GetJobResponse(j.Id, j.Title, j.Description)).ToList();
        return Result<List<GetJobResponse>>.Success(jobResponses);
    }
}