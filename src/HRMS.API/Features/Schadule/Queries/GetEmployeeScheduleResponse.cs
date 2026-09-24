using MediatR;

namespace HRMS.API.Features.Schadule.Queries;
public record GetEmployeeSchaduleResponse(
    Guid EmployeeId,
    string empJob,
    DateOnly Date,
    string  siteName,
    string shiftName ,
    TimeOnly startTime,
    TimeOnly endTime,
    TimeOnly startBreak,
    TimeOnly endBreak
         ) ;
