using Repositories.Models;
using Repositories.Repositores.Abstracts;
using Repositories.Repositores.Implements;

namespace Repositories;

class Program
{
    static void Main(string[] args)
    {
        IMovieRepository repo = new MovieRepository();

        repo.Create(new Movie
        {
            Id = 9,
            Title = "Avatar",
            GenreId = 5,
            ReleaseYear = 2009,
            Duration = 162,
            Director = "James Cameron",
            Description = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
            AvarageRating = 7.9
        });
    }
}

