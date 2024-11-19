using Interview_Server.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace Interview_Server.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            {
                using var client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("i6232853@gmail.com", "InterviewManager1234")
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("i6232853@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(to);
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
