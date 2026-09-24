namespace HRMS.API.Entities;
public class Attendance : BaseEntity
{
    public DateOnly Date { get; set; }

    public DateTime? CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}