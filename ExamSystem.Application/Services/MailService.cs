using ExamSystem.Application.Dtos.MailDtos;
using ExamSystem.Application.Responses;
using ExamSystem.Application.Services.Contract;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;using MailKit.Security;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)        {
            this._mailSettings = (mailSettings ?? throw new ArgumentNullException(nameof(mailSettings))).Value;        }
        

        public BaseResponse<string> SendMail(MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();

                if (!string.IsNullOrEmpty(_mailSettings.Mail))
                    email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
                if (!_mailSettings.TestMode)
                {
                    email.To.Add(new MailboxAddress(mailRequest.ToEmail, mailRequest.ToEmail));
                }
                else
                {
                    if (!string.IsNullOrEmpty(_mailSettings.TestEmail))
                        email.To.Add(new MailboxAddress(_mailSettings.TestEmail, _mailSettings.TestEmail));
                    else
                        email.To.Add(new MailboxAddress(mailRequest.ToEmail, mailRequest.ToEmail));
                }

                if (!string.IsNullOrEmpty(_mailSettings.Bcc))
                    email.Bcc.Add(new MailboxAddress(_mailSettings.Bcc, _mailSettings.Bcc));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                if (mailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }
                if (mailRequest.StreamAttachments != null)
                {
                    foreach (var file in mailRequest.StreamAttachments)
                    {
                        builder.Attachments.Add(file.FileName, file.Attachment);
                    }
                }

                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();

                if (!_mailSettings.TestMode)
                {
                    //using var smtp = new SmtpClient();
                    using var smtp = new SmtpClient();
                    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                    // smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                else
                {
                    if (!string.IsNullOrEmpty(_mailSettings.TestEmail))
                    {
                        using var smtp = new SmtpClient();
                        smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                        // smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                        smtp.Send(email);
                        smtp.Disconnect(true);
                    }
                    else
                    {
                        var pickupPath = string.IsNullOrEmpty(_mailSettings.PickupPath) ?
                            Path.Combine("C:\\", "Inbox") :
                            _mailSettings.PickupPath;
                        if (!Directory.Exists(pickupPath))
                            Directory.CreateDirectory(pickupPath);
                        email.WriteTo(Path.Combine(pickupPath, DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".eml"));
                    }
                }

                return new BaseResponse<string>("mail send successfully");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
