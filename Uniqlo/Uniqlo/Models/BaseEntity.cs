using System;
namespace Uniqlo.Models
{
	public abstract class BaseEntity
	{
		public int Id{ get; set; }
		public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
		public bool isDeleted { get; set; } 
	}
}

