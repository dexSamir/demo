using System;
namespace AdoNet.Models
{
	public class Movie
	{
		public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int ReleaseYear { get; set; }
        public int Duration { get; set; }
        public string director{ get; set; }
        public string Description{ get; set; }
        public double AvarageRating{ get; set; }


        public Movie()
		{

		}
	}
}

