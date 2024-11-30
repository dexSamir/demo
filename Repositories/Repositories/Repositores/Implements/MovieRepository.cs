using System;
using System.Data;
using Repositories.Helper;
using Repositories.Models;
using Repositories.Repositores.Abstracts;

namespace Repositories.Repositores.Implements
{
    public class MovieRepository : IMovieRepository
    {
        public bool Create(Movie movie)
        {
            return SqlHelper.Exec($"insert into movies values({movie.Id}, '{movie.Title}', {movie.GenreId}, {movie.ReleaseYear}, {movie.Duration}, '{movie.Director}', '{movie.Description}' , {movie.AvarageRating})");
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetAll()
        {
            List<Movie> movies = new(); 
            var datatable = SqlHelper.Read("select * from movies");
            foreach (DataRow item in datatable.Rows)
            {
                movies.Add(new Movie
                {
                    Id = Convert.ToInt32(item[0]),
                    Title = item[1].ToString(),
                    GenreId = Convert.ToInt32(item[2]),
                    ReleaseYear = Convert.ToInt32(item[3]),
                    Duration = Convert.ToInt32(item[4]),
                    Director = item[5].ToString(),
                    Description = item[6].ToString(),
                    AvarageRating = Convert.ToInt32(item[7])
                }) ;
            }
            return movies;
        }

        public Movie? getById(int id)
        {
            var dt = SqlHelper.Read("select * from movies where movieid = " + id);
            if(dt.Rows.Count > 0)
            {
                return new Movie
                {
                    Id = Convert.ToInt32(dt.Rows[0]),
                    Title = dt.Rows[1].ToString(),
                    GenreId = Convert.ToInt32(dt.Rows[2]),
                    ReleaseYear = Convert.ToInt32(dt.Rows[3]),
                    Duration = Convert.ToInt32(dt.Rows[4]),
                    Director = dt.Rows[5].ToString(),
                    Description = dt.Rows[6].ToString(),
                    AvarageRating = Convert.ToInt32(dt.Rows[7])
                };
            }
            return null; 
        }

        public bool Update(int id, Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}

