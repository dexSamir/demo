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
            _client = new(opt.Host, opt.Port); 
            _client.Credentials = new NetworkCredential(opt.Sender, opt.Password);
            _client.EnableSsl = true;
            _from = new MailAddress( opt.Sender , "Uniqlo");
            Context = _acc.HttpContext;
        }

        public void SendEmailCinfirmation(string reciever, string name, string token)
        {
            MailAddress to = new(reciever);
            MailMessage msg = new MailMessage(_from, to);
            msg.Subject = "Confirm your email address";
            string url = Context.Request.Scheme + "://" + Context.Request.Host + "/" + "/Account/VerifyEmail?token=" + token + "&user=" + name ; 
            msg.Body = EmailTemplate.VerifyEmail.Replace("__$name" , name).Replace("__$link",url );
            _client.Send(msg); 
        }
    }
}

