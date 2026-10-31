using HRMS.API.Enums;

namespace HRMS.API.Entities;

public class SwapRequest : BaseEntity
{
    public Guid FromEmployeeScheduleId {get;set;}
    public EmployeeSchedule? FromEmployeeSchedule {get;set;}
    public Guid ToEmployeeScheduleId {get;set;}
    public EmployeeSchedule? ToEmployeeSchedule {get;set;}

    public ShiftStatus Status {get;set;} = ShiftStatus.PendingSwap; 
}