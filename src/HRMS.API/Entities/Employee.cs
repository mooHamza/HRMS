namespace HRMS.API.Entities;

using Microsoft.AspNetCore.Identity;

public class Employee : IdentityUser<Guid>
{

    public string? FName { get; set; }
    public string? SName { get; set; }
    public bool MustChangePassword { get; set; } = true;

    public Guid JobId { get; set; }
    public Job? Job { get; set; }
    
    public Guid? ManagerId { get; set; }
    public Employee? Manager { get; set; }
    public Guid DepartmentId { get; set; }
    public Department? Department { get; set; }
    public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; } = [];
    public ICollection<Attendance> Attendances { get; set; } = [];
    public  ICollection<Request> Requests { get; set; } = [];
    public  ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<PasswordResetOtp> PasswordResetOtps { get; set; } = [];
    public ICollection<PasswordResetSession> PasswordResetSessions { get; set; } = [];

}