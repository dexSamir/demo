using System;
using Microsoft.EntityFrameworkCore;
using Uniqlo.Models;

namespace Uniqlo.DataAccess
{
	public class UniqloDbContext : DbContext
	{
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public UniqloDbContext(DbContextOptions opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImage>(
                x => x.Property(y => y.CreatedTime)
                .HasDefaultValueSql("GETDATE()"));
            base.OnModelCreating(modelBuilder);
        }
    }

}

