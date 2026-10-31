using HRMS.API.common;
using HRMS.API.Data;
using HRMS.API.Features.Attendence.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Features.Attendence.Commands;

public class WatchLocationCommandHandler(AppDbContext _context)
    : IRequestHandler<WatchLocationCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        WatchLocationCommand request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var today = DateOnly.FromDateTime(now);
        var currentTime = TimeOnly.FromDateTime(now);

        // Validate coordinates
        if (!double.IsFinite(request.Latitude) ||
            !double.IsFinite(request.Longitude) ||
            request.Latitude < -90 || request.Latitude > 90 ||
            request.Longitude < -180 || request.Longitude > 180)
        {
            return Result<string>.Failure("Invalid coordinates.");
        }

        var schedule = await _context.EmployeeSchedules
            .Include(s => s.Site)
            .Include(s => s.Shift)
            .FirstOrDefaultAsync(
                s => s.EmployeeId == request.EmployeeId &&
                     s.Date == today,
                cancellationToken);

        if (schedule is null)
        {
            return Result<string>.Failure(
                "No schedule found for today.");
        }

        double distance =
            CalculateAllowedDistance.CalculateDistanceInMeters(
                request.Latitude,
                request.Longitude,
                (double)schedule.Site.Latitude,
                (double)schedule.Site.Longitude
            );

        bool isWithinAllowedDistance =
            distance <= AttendenceConstants.AllowedDistanceInMeters;

        bool isOnBreak =
            currentTime >= schedule.Shift.StartBreak &&
            currentTime <= schedule.Shift.EndBreak;

        if (isWithinAllowedDistance || isOnBreak)
        {
            return Result<string>.Success(
                "You are within the allowed distance or currently on break.");
        }

        return Result<string>.Failure(
            $"You are {distance:F0} meters away from the site and are not on break.");
    }
}