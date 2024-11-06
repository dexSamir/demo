using System;
using System.Security.Cryptography.X509Certificates;
using AccountUser.interfaces;

namespace AccountUser
{
	public class User : IAccount
	{
		private int _id;
		public int ID { get; set; }

		public string Fullname;
		public string Email;
		public string Password;

		
		public User(string fullname, string email, string password)
		{
			_id++;
			ID = _id;
			Fullname = fullname;
			Email = email;
			Password = password; 

		}

        public bool PasswordChecker(string password)
        {
            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;

            if (password.Length <= 8)
            {
                return false;
            }

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUpper = true;
                }
                if (char.IsLower(c))
                {
                    hasLower = true;
                }
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
            }

            return hasUpper && hasLower && hasDigit;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Id:{ID} Fullname:{Fullname} Email:{Email}");
        }
    }
}

