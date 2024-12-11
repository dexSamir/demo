using System;
using System.ComponentModel.DataAnnotations;

namespace UniqloProject.ViewModel.Auths
{
	public class ResetPasswordVM
	{
		public string Email { get; set; } 
		public string Token { get; set; }
		[Required, DataType(DataType.Password)]
		public string newPassword { get; set; }
		[Required, DataType(DataType.Password), Compare("newPassword")]
		public string ConfirmPassword { get; set; }
    }
}

