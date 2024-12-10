using System;
namespace UniqloProject.Models
{
	public class Comment : BaseEntity
	{
		public string Content { get; set; } = null!;
        public bool isEdited { get; set; }
		public int Like { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; } = null!;
        public string UserId { get; set; } = null!;
		public User User { get; set; } = null!;

    }
}

