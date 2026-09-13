using FridayFilm.Application.Abstracts.Notifications;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net;

namespace FridayFilm.Infrastructure.Services;

public sealed class EmailService(IConfiguration configuration) : IEmailService
{
    private string Required(string key) =>
        !string.IsNullOrWhiteSpace(configuration[key]) ? configuration[key]!
        : throw new InvalidOperationException($"Missing configuration: {key}");

    public async Task SendVerificationEmailAsync(string toEmail, string userId, string token)
    {
        var sender = Required("EmailSettings:Email");
        var password = Required("EmailSettings:Password");
        var host = Required("EmailSettings:Host");
        if (!int.TryParse(Required("EmailSettings:Port"), out var port) || port is < 1 or > 65535)
            throw new InvalidOperationException("EmailSettings:Port is invalid.");
        var baseUrl = Required("ApplicationUrls:FrontendBaseUrl");
        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri) || uri.Scheme is not ("https" or "http"))
            throw new InvalidOperationException("ApplicationUrls:FrontendBaseUrl is invalid.");
        var link = $"{baseUrl.TrimEnd('/')}/?userId={Uri.EscapeDataString(userId)}&token={Uri.EscapeDataString(token)}#verify-email";
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(sender));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = "FridayFilm — Email təsdiqi";
        message.Body = new BodyBuilder
        {
            TextBody = $"Hesabınızı təsdiqləmək üçün linki açın: {link}",
            HtmlBody = $"<h1>FridayFilm-ə xoş gəldiniz!</h1><p><a href='{WebUtility.HtmlEncode(link)}'>Emailimi təsdiqlə</a></p>"
        }.ToMessageBody();
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(host, port, port == 465 ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(sender, password);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}
