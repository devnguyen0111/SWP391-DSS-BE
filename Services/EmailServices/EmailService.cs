using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Model.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Services.OtherServices;
using Stripe;
using System.Text.Unicode;
using MimeKit.Encodings;
using MimeKit.Utils;

namespace Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IPINCode _pinCode;
        public EmailService(IConfiguration config, IPINCode pinCode)
        {
            this._config = config;
            this._pinCode = pinCode;
        }
        public void SendEmail(Email request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendPinCode(string request, string pin)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request));
            email.Subject = _config.GetSection("PinCodeSubject").Value;
            email.Body = new TextPart(TextFormat.Html) { Text = _config.GetSection("PinCodeBody").Value + "</br>" + _pinCode.PinCodeHtmlContent(pin) };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
