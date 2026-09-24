namespace HRMS.API.Entities;

public class EmployeeSchedule : BaseEntity
{

    // Employee
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    // Shift
    public Guid ShiftId { get; set; }
    public Shift? Shift { get; set; }

    // Site
    public Guid SiteId { get; set; }
    public Site? Site { get; set; }

    // Schedule date
    public DateOnly Date { get; set; }
}