namespace HRMS.API.Entities;

public class Site : BaseEntity
{

    public string? Name { get; set; }

    public Double Latitude { get; set; }

    public Double Longitude { get; set; }

    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; } = [];
}