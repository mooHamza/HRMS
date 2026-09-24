using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HRMS.API.Entities;
using Microsoft.AspNetCore.Identity;
namespace HRMS.API.Data;
public class AppDbContext : IdentityDbContext<Employee, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Attendance> Attendences { get; set; }
    public DbSet<EmployeeSchedule> EmployeeSchedules { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<JobQualification> JobQualifications { get; set; }
    public DbSet<JobPerformanceMetrics> JobPerformanceMetrics { get; set; }
    public DbSet<OvertimeRequest> OvertimeRequests { get; set; }
    public DbSet<SickLeaveRequest> SickLeaveRequests { get; set; }
    public DbSet<RemoteWorkRequest> RemoteWorkRequests { get; set; }
    public DbSet<RequestShiftSwap> ShiftSwapRequests { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }
    public DbSet<PasswordResetSession> PasswordResetSessions { get; set; }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmployeeSchedule>()
        .HasKey(es => new { es.EmployeeId, es.Date,es.ShiftId, es.SiteId });
        modelBuilder.Entity<RequestShiftSwap>()
        .HasKey(r => new { r.FromEmployeeScheduleId, r.ToEmployeeScheduleId });


        modelBuilder.Entity<Request>().OwnsOne(r => r.OvertimeRequest, or =>
        {
            or.WithOwner().HasForeignKey("RequestId");
            or.Property<Guid>("RequestId");
            or.HasKey("RequestId");
        });
        modelBuilder.Entity<Request>().OwnsOne(r => r.RemoteWorkRequest, or =>
        {
            or.WithOwner().HasForeignKey("RequestId");
            or.Property<Guid>("RequestId");
            or.HasKey("RequestId");
        });
        modelBuilder.Entity<Request>().OwnsOne(r => r.SickLeaveRequest, or =>
        {
            or.WithOwner().HasForeignKey("RequestId");
            or.Property<Guid>("RequestId");
            or.HasKey("RequestId");
        });
            
    }

}