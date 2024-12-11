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
        readonly SmtpOptions _smtp;
        readonly SmtpClient _client;
        readonly MailAddress _from;
        readonly HttpContext Context;

        public EmailService(IOptions<SmtpOptions> option, IHttpContextAccessor _acc)
        {
            var opt = option.Value;
            _client = new("smtp.gmail.com", 587); 
            _client.Credentials = new NetworkCredential("samirah-bp215@code.edu.az", "zyjm appz kraq nfrs");
            _client.EnableSsl = true;
            _from = new MailAddress( opt.Sender , "Uniqlo");
            Context = _acc.HttpContext;
        }

        public void SendEmailConfirmation(string reciever, string name, string token)
        {
            MailAddress to = new(reciever);
            MailMessage msg = new MailMessage(_from, to);
            msg.Subject = "Confirm your email address";
            string url = Context.Request.Scheme + "://" + Context.Request.Host + "/" + "Account/ResetPassword?token=" + token + "&email=" + name ; 
            msg.Body = EmailTemplate.VerifyEmail.Replace("__$name" , name).Replace("__$link",url );
            _client.Send(msg); 
        }

        public void SendEmailResetPassword(string to, string username, string resetLink)
        {
            MailAddress reciever = new(to);
            MailMessage msg = new MailMessage(_from, reciever);
            msg.Subject = "Reset Your Password";
            msg.Body = $"Hello {username},<br/><br/>Please reset your password by clicking the link below:<br/><a href='{resetLink}'>Reset Password</a>";
            msg.IsBodyHtml = true;
            _client.Send(msg);
        }
    }
}

