using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniqloProject.DataAccess;
using UniqloProject.Extension;
using UniqloProject.Helpers;
using UniqloProject.Models;
using UniqloProject.Services.Abstracts;
using UniqloProject.Services.Implements;

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
            opt.SignIn.RequireConfirmedEmail = true; 
            opt.Password.RequiredLength = 3;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequireDigit = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireLowercase = true;
            opt.Lockout.MaxFailedAccessAttempts = 10;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1); 
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<UniqloAppDbContext>();
        builder.Services.ConfigureApplicationCookie(x =>
        {
            x.AccessDeniedPath = "/Home/AccessDenied";
        });

        SmtpOptions option = new();
        builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.Name));
        var opt = new SmtpOptions(); 
        builder.Services.AddScoped<IEmailService, EmailService>();


        var app = builder.Build();
        app.UseUserSeed(); 
        app.MapControllerRoute(
            name: "register",
            pattern: "register",
            defaults: new { controller = "Account", action = "Register" });

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

