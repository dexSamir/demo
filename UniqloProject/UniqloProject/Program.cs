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
            opt.SignIn.RequireConfirmedEmail = false; 
            opt.Password.RequiredLength = 3;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = true;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireLowercase = false;
            opt.Lockout.MaxFailedAccessAttempts = 20;
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

        //builder.Services.AddSession(); 

        var app = builder.Build();
        app.UseUserSeed();

        //app.UseSession(); 
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

