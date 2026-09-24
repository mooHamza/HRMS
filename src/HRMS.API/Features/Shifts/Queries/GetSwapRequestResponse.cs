

namespace HRMS.API.Features.Shifts.Queries;
public record SwapRequestResponse
{
    public string ToEmployeeName;
  public DateOnly FromDate;
  public DateOnly ToDate;
  public string FromSiteName;
  public string ToSiteName;
  public string FromShiftName ;
  public string ToShiftName;
    public TimeOnly FromStartTime;
    public TimeOnly ToStartTime;

    public TimeOnly FromStartBreak;
    public TimeOnly ToEndBreak;

}