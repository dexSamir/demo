using System;
namespace Task
{
	public class Library
	{
		Book[] books;
		public string name; 
		public Library(string name)
		{
			this.name = name;
			books = new Book[0]; 
		}
		public Book[] AddBook(Book book)
		{
			Book[] newBooks = new Book[books.Length + 1];
			for(int i = 0; i < books.Length; i++)
			{
				newBooks[i] = books[i];

			}
			newBooks[newBooks.Length - 1] = book;
			books = newBooks;
			return books; 
		}
		public Book[] GetFilteredBooks(string genre)
		{
			Book[] filterByGenre = new Book[books.Length];
			int index = 0; 
			for(int i = 0; i < books.Length; i++)
			{
				if (books[i].genre == genre)
				{
					filterByGenre[index] = books[i];
					index++; 
				}
			}
            Array.Resize(ref filterByGenre, index);
            foreach (var book in filterByGenre)
            {
                Console.WriteLine($"Name:{book.name} Genre:{book.genre} Id:{book.No} Price:{book.Price}");
            }
            return filterByGenre;
        }

		public int GetFilteredBooks(int minPrice, int maxPrice)
		{
			int index = 0; 
			for(int i = 0; i < books.Length ; i++)
			{
				if (books[i].Price >= minPrice && books[i].Price <= maxPrice)
				{
					index++; 
				}
			}
			return index; 

		}

		public void ShowAllBooks()
		{
			foreach (var item in books)
			{
				Console.WriteLine($"Name:{item.name} Genre:{item.genre} Id:{item.No} Price:{item.Price} ");

			}
		}

    }
}

