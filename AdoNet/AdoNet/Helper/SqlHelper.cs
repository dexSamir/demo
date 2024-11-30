using System;
using System.Data;
using Npgsql;

namespace AdoNet.Helper
{
	static class SqlHelper
	{
        const string conString = "Server=localhost;Port=5432;Database=MovieSite;Username=postgres;Password=hebibovs13;";


		public static bool Exec(string command)
		{
			int result; 
			using(NpgsqlConnection connection = new(conString))
			{
				using(NpgsqlCommand cmd = new(command, connection))
				{
					connection.Open();
					result = cmd.ExecuteNonQuery(); 
				}
			}
			return result > 0; 
		}
		public static DataTable Read(string querry)
		{
			DataTable dt = new(); 
			using(NpgsqlConnection connection = new(conString))
			{
				using(NpgsqlDataAdapter sda = new(querry, connection))
				{
					connection.Open();
					sda.Fill(dt); 
				}
			}
			return dt; 
		}
	}
}

