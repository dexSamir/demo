using EFCore.Contexts;
using EFCore.Models;

namespace EFCore;

class Program
{
    static void Main(string[] args)
    {
        List<ToDo> todos = new List<ToDo>();
        
        using (AppDbContext sql = new())
        {
            todos = sql.Salam.ToList(); 
        }
        todos.ForEach(x => Console.WriteLine(x.Title)); 
    }
}

