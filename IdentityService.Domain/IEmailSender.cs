namespace IdentityService.Domain;

public interface IEmailSender
{
    public Task SendAsync(string toEmail, string subject, string body);
}