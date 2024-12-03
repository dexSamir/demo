using System;
using System.ComponentModel.DataAnnotations;

namespace UniqloProject.Models
{
	public abstract class BaseEntity
	{
        [Key]
        public int Id { get; set; }
		public DateTime CreatedTime { get; set; } = DateTime.UtcNow; 
		public bool isDeleted { get; set; } 
	}
}

