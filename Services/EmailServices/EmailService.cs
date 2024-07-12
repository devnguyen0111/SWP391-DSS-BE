using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Services.OtherServices;
using System.Globalization;
using Microsoft.Extensions.Logging;
using MimeKit.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using Model.Models;
using Azure.Core.Pipeline;
using HarfBuzzSharp;
using System.Reflection.Metadata;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.html;
using Microsoft.AspNetCore.Hosting;

namespace Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IEmailRelatedService _emailRelatedService;
        private readonly ILogger<EmailService> _logger;
        private readonly string _webRootPath;


        public EmailService(IConfiguration config, IEmailRelatedService emailRelatedService, ILogger<EmailService> logger, IWebHostEnvironment _env)
        {
            this._config = config;
            this._emailRelatedService = emailRelatedService;
            this._logger = logger;
            _webRootPath = _env.WebRootPath;
        }
        public void SendEmail(Email requestedEmail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail.To));
            email.Subject = requestedEmail.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = requestedEmail.Body };

            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

                _logger.LogInformation("Email sent successfully to {Email}", requestedEmail.To);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", requestedEmail.To);
            }
        }

        public void SendPinCode(string requestedEmail, string pin)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail));
            email.Subject = _config.GetSection("PinCodeSubject").Value;
            email.Body = new TextPart(TextFormat.Html) { Text = _config.GetSection("PinCodeBody").Value + "</br>" + _emailRelatedService.PinCodeHtmlContent(requestedEmail , pin) };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendConfirmationLink(string requestedEmail, string confirmation)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail));
            email.Subject = _config.GetSection("ConfirmationSubject").Value;
            email.Body = new TextPart(TextFormat.Html) { Text = _config.GetSection("ConfirmationBody").Value + "</br><hr></br><a href=" + confirmation + ">" + _config.GetSection("ConfirmationLink").Value + "</a>" };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        public byte[] GeneratePdfFromHtml(string htmlTemplate, Dictionary<string, string> replacements)
        {
            string contentRootPath = Path.Combine(_webRootPath, htmlTemplate);
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(contentRootPath))
            {
                body = reader.ReadToEnd();
            }

            foreach (var replacement in replacements)
            {
                body = body.Replace(replacement.Key, replacement.Value);
            }

            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document();
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                using (var strReader = new StringReader(body))
                {
                    HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);
                    htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

                    ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                    string cssPath = Path.Combine(_webRootPath, "css/site.css");
                    cssResolver.AddCssFile(cssPath, true);

                    IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));
                    var worker = new XMLWorker(pipeline, true);
                    var xmlParse = new XMLParser(true, worker);
                    xmlParse.Parse(strReader);
                    xmlParse.Flush();
                }
                document.Close();
                return ms.ToArray();
            }
        }

        public void SendEmailWithAttachment(Email requestedEmail, byte[] attachmentBytes, string attachmentName)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail.To));
            email.Subject = requestedEmail.Subject;

            var bodyPart = new TextPart(TextFormat.Html)
            {
                Text = requestedEmail.Body
            };

            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(new MemoryStream(attachmentBytes)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = attachmentName
            };
            var multipart = new MimeKit.Multipart("mixed")
            {
                bodyPart,
                attachment
            };

            email.Body = multipart;

            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

                _logger.LogInformation("Email sent successfully to {Email}", requestedEmail.To);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", requestedEmail.To);
            }
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

        public void SendResetLink(string requestedEmail, string resetUrl)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail));
            email.Subject = _config.GetSection("ResetPasswordSubject").Value;
            email.Body = new TextPart(TextFormat.Html) { Text = _emailRelatedService.ResetPasswordContent(requestedEmail, resetUrl) };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }

        public void SendGIA(string requestedEmail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(requestedEmail));
            email.Subject = _config.GetSection("ResetPasswordSubject").Value;
            email.Body = new TextPart(TextFormat.Html) { Text = _emailRelatedService.GIAContent() };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
