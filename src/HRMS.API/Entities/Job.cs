namespace HRMS.API.Entities;

public class Job : BaseEntity
{
    public string? Title { get; set; }
    public string?  Description { get; set; }

    public ICollection<JobQualification> JobQualifications { get; set; } = [];

    public ICollection<JobPerformanceMetrics> JobPerformanceMetrics { get; set; } = [];
}