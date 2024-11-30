using System;
namespace EFCore.Models
{
	public class ToDo
	{
		public int Id{ get; set; }
		public string Title { get; set; } = null!;
		public string? Description{ get; set; }
		public DateTime DeadLine { get; set; }



		public ToDo()
		{
		}
	}
}

