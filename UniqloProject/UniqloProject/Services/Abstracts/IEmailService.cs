using System;
namespace UniqloProject.Services.Abstracts
{
	public interface IEmailService
	{
        void SendEmailCinfirmation(string reciever, string name, string token); 
	}
}

