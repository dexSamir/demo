using System;
using Microsoft.AspNetCore.Identity;

namespace UniqloProject.Models
{
	public class User : IdentityUser
	{
		public string Email { get; set; } = null!;
		public string Fullname{ get; set; } = null!;
        public string? ProfileImageUrl { get; set; }
		public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RePassword { get; set; } = null!;
    }
}

