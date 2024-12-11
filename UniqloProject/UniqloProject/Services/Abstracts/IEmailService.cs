using System;
namespace UniqloProject.Services.Abstracts
{
	public interface IEmailService
	{
        void SendEmailConfirmation(string reciever, string name, string token);
        void SendEmailResetPassword(string to, string username, string resetLink);

    }
}

