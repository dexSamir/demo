using System;
using Repositories.Models;

namespace Repositories.Repositores.Abstracts
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();

        Movie? getById(int id);

        bool Create(Movie movie);
        bool Delete(int id);
        bool Update(int id, Movie movie);

    }
}

