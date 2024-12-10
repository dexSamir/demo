using System;
using Microsoft.AspNetCore.Identity;

namespace UniqloProject.Models
{
	public class User : IdentityUser
	{
		public string Fullname{ get; set; } = null!;
        public string? ProfileImageUrl { get; set; }
		public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>(); 
    }
}

