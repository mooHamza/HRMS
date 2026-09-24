namespace HRMS.API.Entities;
public class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}