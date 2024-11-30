using System;
using Repositories.Models;
using Repositories.Repositores.Abstracts;

namespace Repositories.Repositores.Implements
{
	public class MovieOracle : IMovieRepository 
	{
		public MovieOracle()
		{
		}

        public bool Create(Movie movie)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetAll()
        {
            throw new NotImplementedException();
        }

        public Movie? getById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}

