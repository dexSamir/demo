using System;
namespace UniqloProject.Models
{
	public class Comment : BaseEntity
	{
		public string Content { get; set; } = null!;
        public bool isEdited { get; set; }
		public int Like { get; set; }

		public int? ProductId { get; set; }
		public Product? Product { get; set; } 
        public string? UserId { get; set; } 
		public User? User { get; set; } 

    }
}

