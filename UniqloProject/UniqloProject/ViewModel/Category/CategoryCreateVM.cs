using System;
using System.ComponentModel.DataAnnotations;

namespace UniqloProject.ViewModel.Category
{
	public class CategoryCreateVM
	{
		[MaxLength(32, ErrorMessage ="Name must be less than 32 charachters"), Required(ErrorMessage = "Name is Required")]
		public string Name { get; set; } = null!; 
	}
}

