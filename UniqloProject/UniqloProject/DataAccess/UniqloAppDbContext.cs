using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniqloProject.Models;

namespace UniqloProject.DataAccess
{

    public class UniqloAppDbContext : IdentityDbContext<User>
    {
		public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public UniqloAppDbContext(DbContextOptions<UniqloAppDbContext> options) : base(options) { }

    }
}
