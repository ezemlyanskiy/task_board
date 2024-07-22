namespace Application.Common.Interfaces.Services.Email;

public interface IEmailSender
{
    Task SendEmailAsync(Message message);
}
