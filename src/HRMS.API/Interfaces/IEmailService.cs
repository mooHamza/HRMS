namespace HRMS.API.Features.Auth.Services;
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}