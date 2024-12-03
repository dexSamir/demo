﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniqloProject.DataAccess;
using UniqloProject.Models;

namespace UniqloProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<UniqloAppDbContext>(opt =>
        {
            opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")); 
        });

        builder.Services.AddIdentity<User, IdentityRole>(opt =>
        {
            opt.Password.RequiredLength = 3;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequireDigit = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<UniqloAppDbContext>(); 

        var app = builder.Build();
        app.MapControllerRoute(
            name: "register",
            pattern: "register",
            defaults: new { controller = "Account", action = "Register" }
            );

        app.MapControllerRoute(
            name:"areas",
            pattern:"{area:exists}/{controller=Dashboard}/{action=Index}"
            );
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.UseStaticFiles(); 
        app.Run();
    }
}
