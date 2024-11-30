using System;
namespace Repositories.Models
{
	public class Movie
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int ReleaseYear { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public double AvarageRating { get; set; }

	}
}

