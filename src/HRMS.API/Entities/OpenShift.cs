using System.ComponentModel.DataAnnotations;
using HRMS.API.Enums;

namespace HRMS.API.Entities;

public class OpenShift : BaseEntity
{
    public Guid ScheduleId { get; set; }
    public Guid ReleasedByEmployeeId { get; set; }
    public Guid? ClaimedByEmployeeId { get; set; }
    public Guid JobId { get; set; }

    // Shift
    public Guid ShiftId { get; set; }

    // Site
    public Guid SiteId { get; set; }

    // Schedule date
    public DateOnly Date { get; set; }

    public ShiftStatus Status {get; set;} = ShiftStatus.Open;
     
    [Timestamp]
    public   byte[] RowVersion { get; set; } = [];
}