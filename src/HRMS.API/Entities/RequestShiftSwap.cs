using HRMS.API.Enums;

namespace HRMS.API.Entities;

public class RequestShiftSwap
{
    public Guid FromEmployeeScheduleId {get;set;}
    public EmployeeSchedule? FromEmployeeSchedule {get;set;}
    public Guid ToEmployeeScheduleId {get;set;}
    public EmployeeSchedule? ToEmployeeSchedule {get;set;}

    public RequestStatus Status {get;set;} = RequestStatus.Pending; 
}