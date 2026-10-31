namespace HRMS.API.Features.Schadule.Queries.GetAllTeamSchadule;
public record GetAllTeamScheduleResponse(
    Guid ScheduleId,
    Guid EmployeeId,
    string EmployeeName,
    string EmployeeJob,
    DateOnly Date,

    string ShiftName,
    string SiteName,
    
    TimeOnly StartTime,
    TimeOnly EndTime,
    TimeOnly StartBreak,
    TimeOnly EndBreak );