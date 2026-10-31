using HRMS.API.Features.Attendence.Enums;

namespace HRMS.API.Features.Attendence.Queries.GetEmployeeAttendence;

public class GetEmployeeAttendenceResponse
{
    public DateOnly Date { get; set; }
    public TimeOnly? CheckIn { get; set; }
    public TimeOnly? CheckOut { get; set; }

    public TimeSpan LateCheckIn { get; set; }
    public TimeSpan WorkHours { get; set; }
    public string Status { get; set; }

    public TimeOnly StartBreak { get; set; }
    public TimeOnly EndBreak { get; set; }
}

