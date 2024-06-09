﻿using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Services.OtherServices;

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
        public void SendEmail(Email requestedEmail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail.To));
            email.Subject = requestedEmail.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = requestedEmail.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendPinCode(string requestedEmail, string pin)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail));
            email.Subject = _config.GetSection("PinCodeSubject").Value;
            email.Body = new TextPart(TextFormat.Html) { Text = _config.GetSection("PinCodeBody").Value + "</br>" + _pinCode.PinCodeHtmlContent(pin) };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendConfirmation(string requestedEmail, string confirmation)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail));
            email.Subject = _config.GetSection("ConfirmationSubject").Value;
            email.Body = new TextPart(TextFormat.Html) { Text = _config.GetSection("ConfirmationBody").Value + "</br><hr></br><a href=" + confirmation +">" + _config.GetSection("ConfirmationLink").Value + "</a>"};

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendAttachment(string requestedEmail)
        {
            var email = new MimeMessage();
            var builder = new BodyBuilder();

            //download attachment (certificate) from website
            //using (WebClient wc = new WebClient())
            //{
            //    wc.DownloadFile(
            //        // Param1 = Link of file
            //        new System.Uri("đường dẫn ở đây"),       //Change this to the website
            //        // Param2 = Path to save
            //        @"A:\Images\atachment.pdf"               //change this to local
            //    );
            //}

            string path = @"C:\Users\ASUS\Downloads\634787926777-7641681619-ticket.pdf";
            var attachment = new MimePart("file", "pdf")
            {
                Content = new MimeContent(System.IO.File.OpenRead(path)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(path)
            };

            // now create the multipart/mixed container to hold the message text and the
            // image attachment

            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail));
            email.Subject = _config.GetSection("AttachmentSubject").Value;
            email.Body = attachment;

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
