using System;
using System.ComponentModel.DataAnnotations;

namespace UniqloProject.ViewModel.Auths
{
	public class ForgetPasswordVM
	{
		[EmailAddress]
		public string Email { get; set; } = null!;
	}
}

