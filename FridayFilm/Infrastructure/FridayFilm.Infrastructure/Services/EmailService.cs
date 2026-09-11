using FridayFilm.Application.Abstracts.Notifications;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace FridayFilm.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationEmailAsync(string toEmail, string userId, string token)
        {
            Console.WriteLine("============= EMAIL GÖNDƏRİLMƏYƏ BAŞLAYIR =============");

            try
            {
                var senderEmail = _configuration["EmailSettings:Email"];
                var senderPassword = _configuration["EmailSettings:Password"];
                var host = _configuration["EmailSettings:Host"];
                var port = int.Parse(_configuration["EmailSettings:Port"]);

                var encodedToken = Uri.EscapeDataString(token);
                var verifyLink = $"https://localhost:7243/api/auth/verify-email?userId={userId}&token={encodedToken}";

                Console.WriteLine($"KİMƏ GEDİR: {toEmail}");
                Console.WriteLine($"LİNK BUDUR: {verifyLink}");

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(senderEmail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "FridayFilm - Hesabın Təsdiqlənməsi";

                var builder = new BodyBuilder();
                builder.HtmlBody = $"<h1>Xoş gəldiniz!</h1> <a href='{verifyLink}'>Hesabı Təsdiqlə</a>";
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(senderEmail, senderPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

            }
            catch (Exception ex)
            {
               
                throw; 
            }
        }
    }
}