using Microsoft.AspNetCore.Identity;
using MediatR;
using HRMS.API.Entities;
using HRMS.API.common;
using HRMS.API.Data;
using Microsoft.EntityFrameworkCore;
using HRMS.API.Mappers;
using HRMS.API.Features.Auth.DTOs;
namespace HRMS.API.Features.Auth.Commands;

public class RegisterCommandHandler
(
UserManager<Employee> userManager,
AppDbContext dbContext,
EmployeeMapper employeeManager
) : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
   public async Task<Result<RegisterResponse>> Handle(
    RegisterCommand request,
    CancellationToken cancellationToken)
{
    // 1. Check email
    var existingEmployee = await userManager.FindByEmailAsync(request.Email);

    if (existingEmployee is not null)
    {
        return Result<RegisterResponse>.Failure(
            "Email already exists.");
    }

    // 2. Check Job
    var jobExists = await dbContext.Jobs
        .AnyAsync(
            j => j.Id == request.JobId,
            cancellationToken);

    if (!jobExists)
    {
        return Result<RegisterResponse>.Failure(
            "Job does not exist.");
    }

    // 3. Check Department
    var departmentExists = await dbContext.Departments
        .AnyAsync(
            d => d.Id == request.DepartmentId,
            cancellationToken);

    if (!departmentExists)
    {
        return Result<RegisterResponse>.Failure(
            "Department does not exist.");
    }

    // // 4. Check Manager
    
    //     var managerExists = await dbContext.Users
    //         .AnyAsync(
    //             e => e.Id == request.ManagerId,
    //             cancellationToken);

    //     if (!managerExists)
    //     {
    //         return Result<RegisterResponse>.Failure(
    //             "Manager does not exist.");
    //     }
    

    // 5. Create employee
    var newEmployee = employeeManager.ToEmployee(request);

    // 6. Create Identity user
    var identityResult = await userManager.CreateAsync(
        newEmployee,
        request.Password);

    if (!identityResult.Succeeded)
    {
        return Result<RegisterResponse>.Failure(
            identityResult.Errors
                .Select(e => e.Description)
                .ToArray());
    }

    // 7. Response
    return Result<RegisterResponse>.Success(
        employeeManager.ToRegisterResponse(newEmployee));
}
}