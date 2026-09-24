namespace HRMS.API.Entities;

public class Shift : BaseEntity
{

    public string? Name { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
    public TimeOnly StartBreak { get; set; }
    public TimeOnly EndBreak { get; set; }

    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; } = [];

}