namespace HRMS.API.Features.Shifts.Queries.GetMarketShifts;

public class GetMarketShiftsResponse
{
    public Guid ScheduleId { get; set; }

    public string? ShiftName { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
    public TimeOnly StartBreak { get; set; }
    public TimeOnly EndBreak { get; set; }
    public string SiteName { get; set; }
    public DateOnly Date { get; set; }
    public Guid ReleasedByEmployeeId { get; set; }
    public Guid JobId { get; set; }
}