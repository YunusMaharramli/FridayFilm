using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Application.Abstracts.Notifications;

public interface IEmailService
{
    Task SendVerificationEmailAsync(string toEmail, string userId, string token);
}
