using System;
using System.ComponentModel.DataAnnotations;

namespace UniqloProject.Models
{
	public class Slider : BaseEntity
	{
		[MaxLength(32)]
		public string Title { get; set; } = null!;
        [MaxLength(64)]
        public string SubTitle { get; set; } = null!;
		public string? Link { get; set; }
		public string ImageUrl { get; set; } = null!; 
	}
}

