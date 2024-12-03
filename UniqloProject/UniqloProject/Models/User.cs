using System;
using Microsoft.AspNetCore.Identity;

namespace UniqloProject.Models
{
	public class User :IdentityRole
	{
		public string Email { get; set; }
		public string Fullname{ get; set; }
		public string ProfileImageUrl { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string RePassword { get; set; }
    }
}

