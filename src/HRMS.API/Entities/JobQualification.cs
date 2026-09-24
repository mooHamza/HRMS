namespace HRMS.API.Entities;

public class JobQualification : BaseEntity
{
    public string? Qualification { get; set; }
    public Guid JobId { get; set; }
    public Job? Job { get; set; }

   
}