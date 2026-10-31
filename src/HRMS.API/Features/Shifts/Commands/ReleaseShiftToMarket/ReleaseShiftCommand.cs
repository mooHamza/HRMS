using HRMS.API.common;
using HRMS.API.Entities;
using MediatR;

namespace HRMS.API.Features.Shifts.Commands.ReleaseShiftToMarket;

public record ReleaseShiftCommand : IRequest<Result> {
    public Guid ScheduleId { get; set; }
     public Guid ReleasedByEmployeeId  { get; set; }
    public Guid? ClaimedByEmployeeId { get; set; }
    public Guid JobId { get; set; }

    // Shift
    public Guid ShiftId { get; set; }

    // Site
    public Guid SiteId { get; set; }

    // Schedule date
    public DateOnly Date { get; set; }

} 

