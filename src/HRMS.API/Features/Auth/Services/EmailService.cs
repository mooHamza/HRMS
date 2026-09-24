using MailKit.Net.Smtp;
using MimeKit;

namespace HRMS.API.Features.Auth.Services;
public class EmailService
(
    IConfiguration configuration

) : IEmailService
{

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var smtpHost = configuration["EmailSettings:Host"];
        var smtpPort = int.Parse(configuration["EmailSettings:Port"]!);
        var smtpUser = configuration["EmailSettings:Username"];
        var smtpFrom = configuration["EmailSettings:From"];

   var email = new MimeMessage();

        email.From.Add(
            MailboxAddress.Parse(
                smtpFrom!));
    
        email.To.Add(
            MailboxAddress.Parse(to));

        email.Subject = subject;

        email.Body = new TextPart("html")
        {
            Text = body
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            smtpHost!,
            smtpPort,
            MailKit.Security.SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            smtpUser!,
            configuration["EmailSettings:Password"]!);

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}