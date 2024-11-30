using System;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Contexts
{
	public class AppDbContext : DbContext 
	{
		public DbSet<ToDo> Salam { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Server=example;Port=5432;Database=BP215EF;User Id=postgres;Password=hebibovs13;");
			base.OnConfiguring(optionsBuilder);
		}
	}
}

