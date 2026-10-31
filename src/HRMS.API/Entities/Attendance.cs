namespace HRMS.API.Entities;
public class Attendance : BaseEntity
{
    public DateOnly Date { get; set; }

    public TimeOnly? CheckIn { get; set; }

    public TimeOnly? CheckOut { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}