using Interview_Server.Interfaces;
using Interview_Server.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;
using MailKit.Security;

namespace Interview_Server.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task<bool> SendEmailAsync(MailData mailData)
        {
            try
            {
                using (var emailMessage = new MimeMessage())
                {
                    // Sender email
                    var emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                    emailMessage.From.Add(emailFrom);

                    // Recipient email
                    var emailTo = new MailboxAddress(mailData.MailToId, mailData.MailTo);
                    emailMessage.To.Add(emailTo);

                    emailMessage.Subject = mailData.Subject;

                    BodyBuilder bodyBuilder = new BodyBuilder(){
                        TextBody = mailData.Body
                    };
                    emailMessage.Body = bodyBuilder.ToMessageBody();

                    using (var mailClient = new SmtpClient())
                    {

                        await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.StartTls);
                        Console.WriteLine("Connected to the mail server", _mailSettings.Server);
                        await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                        Console.WriteLine("Authenticated with the mail server", _mailSettings.UserName, _mailSettings.Password);
                        await mailClient.SendAsync(emailMessage);
                        Console.WriteLine("Email sent successfully");
                        await mailClient.DisconnectAsync(true);
                    }
                    return true;
                   
                }
                
            }
            catch (MailKit.Security.SslHandshakeException sslEx)
            {
                // Log specific SSL Handshake exception details
                Console.WriteLine($"SSL Handshake error: {sslEx.Message}");
                Console.WriteLine($"Stack Trace: {sslEx.StackTrace}");
                // Optionally, log to a file or a logging framework here
                return false; // Indicating failure
            }
        }
    }
}
