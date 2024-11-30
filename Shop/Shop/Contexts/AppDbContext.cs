using System;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Contexts
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> users { get; set; }
		public DbSet<Product> products { get; set; }
		public DbSet<Basket> baskets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=shop;Username=postgres;Password=hebibovs13");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

