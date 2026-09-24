namespace HRMS.API.Entities;
public class PasswordResetSession
{
    public Guid Id { get; set; }

    public Guid EmployeeId { get; set; }

    public string ResetToken { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; }

    public Employee Employee { get; set; } = null!;
}