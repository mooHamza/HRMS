namespace HRMS.API.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}