
namespace HRMS.API.Entities;
using HRMS.API.Enums;
public class Request : BaseEntity
{
    public RequestTypes RequestType { get; set; }

    public RequestStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public OvertimeRequest? OvertimeRequest { get; set; }

    public SickLeaveRequest? SickLeaveRequest { get; set; }
    public RemoteWorkRequest? RemoteWorkRequest { get; set; }
}