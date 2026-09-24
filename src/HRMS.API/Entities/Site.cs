namespace HRMS.API.Entities;

public class Site : BaseEntity
{

    public string? Name { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; } = [];
}