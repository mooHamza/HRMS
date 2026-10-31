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
    public DbSet<OpenShift> OpenShifts { get; set; }
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
    public DbSet<SwapRequest> SwapRequests { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }
    public DbSet<PasswordResetSession> PasswordResetSessions { get; set; }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<EmployeeSchedule>(entity =>
{
   entity.HasKey(x => x.Id);

    // Prevent duplicate schedules for the same employee,
    // date, shift, and site
    entity.HasIndex(x => new
    {
        x.EmployeeId,
        x.Date,
        x.ShiftId,
        x.SiteId
    }).IsUnique();
});

modelBuilder.Entity<SwapRequest>(entity =>
{
    entity.HasKey(x => x.Id);

    entity.HasOne(x => x.FromEmployeeSchedule)
        .WithMany()
        .HasForeignKey(x => x.FromEmployeeScheduleId)
        .OnDelete(DeleteBehavior.Restrict);

    entity.HasOne(x => x.ToEmployeeSchedule)
        .WithMany()
        .HasForeignKey(x => x.ToEmployeeScheduleId)
        .OnDelete(DeleteBehavior.Restrict);
});
        modelBuilder.Entity<SwapRequest>()
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