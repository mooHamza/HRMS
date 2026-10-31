

namespace HRMS.API.Features.Shifts.Queries;
public record SwapRequestResponse
{
  public Guid FromEmployeeScheduleId { get; init; }
  public Guid ToEmployeeScheduleId { get; init; }
    public string FromEmployeeName { get; init; } = string.Empty;
    public string ToEmployeeName { get; init; } = string.Empty;
    public DateOnly FromDate { get; init; }
    public DateOnly ToDate { get; init; }
    public string FromSiteName { get; init; } = string.Empty;
    public string ToSiteName { get; init; } = string.Empty;
    public string FromShiftName { get; init; } = string.Empty;
    public string ToShiftName { get; init; } = string.Empty;
    public TimeOnly FromStartTime { get; init; }
    public TimeOnly ToStartTime { get; init; }
    public TimeOnly FromStartBreak { get; init; }
    public TimeOnly ToEndBreak { get; init; }
}

