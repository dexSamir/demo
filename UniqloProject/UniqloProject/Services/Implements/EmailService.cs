using System;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Common;
using UniqloProject.Helpers;
using UniqloProject.Services.Abstracts;

namespace UniqloProject.Services.Implements
{
    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _smtpOptions;
        private readonly SmtpClient _smtpClient;
        private readonly MailAddress _fromAddress;
        readonly HttpContext Context;

        public EmailService(IOptions<SmtpOptions> option, IHttpContextAccessor _acc)
        {
            _smtpOptions = option.Value;
            _smtpClient = new(_smtpOptions.Host, _smtpOptions.Port);
            _smtpClient.Credentials = new NetworkCredential(_smtpOptions.Sender, _smtpOptions.Password);
            _smtpClient.EnableSsl = true;
            _fromAddress = new MailAddress( _smtpOptions.Sender , "Uniqlo");
            Context = _acc.HttpContext;
        }

        public void SendEmailConfirmation(string reciever, string name, string token)
        {
            MailAddress to = new(reciever);
            MailMessage msg = new MailMessage(_fromAddress, to);
            msg.Subject = "Confirm your email address";
            string url = token + "&email=" + name; 
            msg.Body = EmailTemplate.VerifyEmail.Replace("__$name" , name).Replace("__$link",url);
            _smtpClient.Send(msg); 
        }

        public void SendEmailResetPassword(string to, string username, string resetLink)
        {
            MailAddress reciever = new(to);
            MailMessage msg = new MailMessage(_fromAddress, reciever);
            msg.Subject = "Reset Your Password";
            msg.Body = $"Hello {username},<br/><br/>Please reset your password by clicking the link below:<br/><a href='{resetLink}'>Reset Password</a>";
            msg.IsBodyHtml = true;
            _smtpClient.Send(msg);
        }
    }
}

