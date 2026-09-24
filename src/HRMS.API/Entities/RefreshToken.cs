namespace HRMS.API.Entities;
public class RefreshToken
{
    public Guid Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsRevoked { get; set; } 

    public Guid EmployeeId { get; set; }

    public Employee Employee { get; set; } = null!;
}