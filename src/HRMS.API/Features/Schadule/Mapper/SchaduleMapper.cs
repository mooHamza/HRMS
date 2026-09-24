using AutoMapper;
using HRMS.API.Entities;
using HRMS.API.Features.Schadule.Queries;


namespace HRMS.API.Features.Schadule.Mapper;

public class SchaduleMapper 
{
    public static GetEmployeeSchaduleResponse ToSchaduleResponses(EmployeeSchedule employeeSchedule)
    {
    return new GetEmployeeSchaduleResponse(
        employeeSchedule.EmployeeId,

         employeeSchedule.Employee!.Job!.Title ?? string.Empty,

        employeeSchedule.Date,
         employeeSchedule.Site?.Name ?? string.Empty,
        employeeSchedule.Shift?.Name ?? string.Empty,
         employeeSchedule.Shift?.StartTime ?? TimeOnly.MinValue,
        employeeSchedule.Shift?.EndTime ?? TimeOnly.MinValue,
        employeeSchedule.Shift?.StartBreak ?? TimeOnly.MinValue,
        employeeSchedule.Shift?.EndBreak ?? TimeOnly.MinValue

           );
    }
}