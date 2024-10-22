using System;
namespace AccountUser.interfaces
{
	public interface IAccount
	{
		public bool PasswordChecker(string password);
		public void ShowInfo(); 

    }
}

