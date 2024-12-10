using System;
using System.Buffers.Text;

namespace UniqloProject.Models
{
	public class Tag : BaseEntity
	{
		public string Name { get; set; }
		public ICollection<Product> Products { get; set; }

	}
}

