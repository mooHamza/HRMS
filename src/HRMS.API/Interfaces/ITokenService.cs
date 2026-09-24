using HRMS.API.Entities;
using HRMS.API.Features.Auth.Commands.Login;


namespace HRMS.API.Features.Auth.Services;

public interface ITokenService
{
    Task<LoginResponse> GenerateAccessTokensAsync(Employee employee);
}