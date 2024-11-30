using System;
using System.Data;
using AdoNet.Helper;
using AdoNet.Models;

namespace AdoNet.Service
{
	public static class MovieService
	{
		public static void Add(Movie movie)
		{

			string querry = $"insert into Movies values({movie.Id}, '{movie.Title}', {movie.GenreId}, {movie.ReleaseYear}, {movie.Duration}, '{movie.director}' , '{movie.Description}', {movie.AvarageRating})";
			SqlHelper.Exec(querry);

		}
		public static List<Movie> GetAllMovies()
		{
			List<Movie> movies = new List<Movie>(); 
			string querry = "Select * from Movies";

			var dt = SqlHelper.Read(querry);

			foreach (DataRow dr in dt.Rows)
			{
				movies.Add(new Movie
				{
					Id = (int)dr["movieid"],
					Title = (string)dr["title"],
					GenreId = (int)dr["genreid"],
					ReleaseYear = (int)dr["releaseyear"],
					Duration = (int)dr["duration"],
					director = (string)dr["director"],
					Description = (string)dr["description"],
					AvarageRating = Convert.ToDouble(dr["ratingaverage"])
					
				}); 
			}
			return movies; 
		}
	}
}

