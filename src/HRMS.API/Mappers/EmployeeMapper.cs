namespace HRMS.API.Mappers;
using HRMS.API.Entities;
using HRMS.API.Features.Auth.DTOs;
public class EmployeeMapper
{
    public Employee ToEmployee(RegisterCommand request)
    {
        return new Employee
        {

            FName = request.FirstName,
            SName = request.LastName,
            UserName = request.Email,
            Email = request.Email,
            JobId = request.JobId,
            ManagerId = request.ManagerId,
            DepartmentId = request.DepartmentId
        };
    }

    public RegisterResponse ToRegisterResponse(Employee employee)
    {
        return new RegisterResponse(
            employee.Id,
            employee.FName ?? string.Empty,
            employee.SName ?? string.Empty,
            employee.Email ?? string.Empty
        );
    }
}