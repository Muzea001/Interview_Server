using Interview_Server.Models;

namespace Interview_Server.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(MailData mailData);
    }
}
