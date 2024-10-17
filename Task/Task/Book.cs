using System;
namespace Task
{
	public class Book : Product
	{
		public string genre;

		public Book( string name, int no, double Price, int Count, string genre)
			:base(name, no, Price, Count)
		{
			this.genre = genre;
					
		}
		public void ShowFullInfo()
		{
			Console.WriteLine($"Name:{name} Genre:{genre} Price:{Price} Id:{No} " );
		}

	}
}

