using System;
using System.ComponentModel.DataAnnotations;

namespace UniqloProject.ViewModel.Product
{
	public class CommentCreateVM
	{
		[MaxLength(400), Required(ErrorMessage = "Content is required")]
		public string Content { get; set; } = null!;
		public int ProductId { get; set; }
		public string UserId { get; set; } 
	}
}

