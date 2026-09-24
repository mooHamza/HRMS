namespace HRMS.API.Entities;

public class PasswordResetOtp
{
    public Guid Id { get; set; }

    public Guid EmployeeId { get; set; }

    public string OtpHash { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; }

    public int FailedAttempts { get; set; }

    public Employee Employee { get; set; } = null!;
}