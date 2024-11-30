using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class TodoController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (AppDbContext context = new())
            {
                return View(await context.Todos.ToListAsync());
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(Todo data) 
        {
            using(AppDbContext context = new())
            {
                await context.Todos.AddAsync(data);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); 
            }
            using(AppDbContext context = new())
            {
                if(await context.Todos.AnyAsync(x => x.Id == id))
                {
                    context.Todos.Remove(new Todo
                    {
                        Id = id.Value,
                    });
                    await context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
