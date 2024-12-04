﻿using System;
using System.ComponentModel.DataAnnotations;

namespace UniqloProject.ViewModel.Auths
{
	public class UserCreateVM
	{
		[Required, MaxLength(64)]
		public string Fullname { get; set; }

        public string Username { get; set; }

		[Required, MaxLength(128), EmailAddress]
		public string Email { get; set; }

		[Required, MaxLength(32), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(32), DataType(DataType.Password), Compare(nameof(Password))]
        public string RePassword { get; set; }
	}
}

