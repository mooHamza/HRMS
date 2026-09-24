namespace HRMS.API.Entities;

public class JobPerformanceMetrics : BaseEntity
{

    public string? MetricName { get; set; }
    
    public int PerformanceScore { get; set; }

     public Guid JobId { get; set; }
      public Job? Job { get; set; }
}