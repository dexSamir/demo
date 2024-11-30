using Microsoft.EntityFrameworkCore;

namespace ToDoApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.MapControllerRoute("default", "{controller=Todo}/{action=Index}");
        app.Run();
    }
}

//dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=BP215EF;User Id=postgres;Password=hebibovs13;" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -c AppDbContext --context-dir Context --force