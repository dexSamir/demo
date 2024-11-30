using System.Data;
using AdoNet.Helper;
using AdoNet.Models;
using AdoNet.Service;
using Dapper;
using Npgsql;

namespace AdoNet;

class Program
{
    const string conString = "Server=localhost;Port=5432;Database=MovieSite;Username=postgres;Password=hebibovs13;";
    static void Main(string[] args)
    {
        //ORM - Object-Related Mapping

        //readWithReader(); 
        //AddMovie(6,"Matrix", 5, 1999, 136, "Wachoski sisters", "When a beautiful stranger leads computer hacker Neo to a forbidding underworld", 8.7); 
        //DeleteMovie(5);
        //ReadDataTable();


        //MovieService.Add(new Movie
        //{
        //    Id = 7,
        //    Title = "Gladiator",
        //    GenreId = 3,
        //    ReleaseYear = 2000,
        //    Duration = 155,
        //    director = "Ridley Scott",
        //    Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",
        //    AvarageRating = 8.5
        //} );

        //MovieService.GetAllMovies().ForEach(x =>
        //{
        //    Console.WriteLine($"{x.Title} {x.ReleaseYear} {x.Duration}" );
        //});

        using (NpgsqlConnection connection = new(conString))
        {
            var movies = connection.Query<Movie>("select * from Movies");
            foreach (var item in movies)
            {
                Console.WriteLine(item.Title);
            }
        }
    }
    //static void ReadDataTable()
    //{
    //    foreach (DataRow item in SqlHelper.Read("select * from movies").Rows)
    //    {
    //        Console.WriteLine(item[0] + " " + item[1] + " " + item[2] + " " + item[3]);
    //    }
    //}
    //static void readWithReader()
    //{
    //    using (NpgsqlConnection connection = new NpgsqlConnection(conString))
    //    {
    //        using (NpgsqlCommand cmd = new NpgsqlCommand("Select * from movies", connection))
    //        {
    //            connection.Open();
    //            var reader = cmd.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4] + " " + reader[5] + " " + reader[6] + " " + reader[7]);
    //            }
    //            connection.Close();

    //        }
    //    }

    //}
    //static void AddMovie(int id, string title, int genreid, int releaseYear,int duration, string director, string description, double ratingPoint )
    //{
    //    using (NpgsqlConnection connection = new(conString))
    //    {
    //        using (NpgsqlCommand cmd = new($"Insert into movies values({id}, '{title}', {genreid}, {releaseYear}, {duration}, '{director}', '{description}', {ratingPoint})", connection))
    //        {
    //            connection.Open();
    //            cmd.ExecuteNonQuery(); 
    //        } 
    //    }
    //}
    //static void DeleteMovie(int id)
    //{
    //    if(SqlHelper.Exec($"Delete from Movies where movieid = {id}"))
    //    {
    //        Console.WriteLine("Movie deleted successfully");
    //    }
    //    else
    //    {
    //        Console.WriteLine("Movie wasn't exist in database");
    //    }
    //}
}

