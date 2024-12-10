using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Versioning;
using UniqloProject.DataAccess;

namespace UniqloProject.Controllers
{
    public class ProductController : Controller
    {
        readonly UniqloAppDbContext _context;
        public ProductController(UniqloAppDbContext context)
        {
            _context = context; 
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Deteails(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products
                .Where(x => x.Id == id && !x.isDeleted)
                .Include(x => x.Images)
                .Include(x => x.Ratings)
                .FirstOrDefaultAsync();
            if (data == null) return NotFound();
            ViewBag.Rating = 5; 
            if(User.Identity?.IsAuthenticated ?? false)
            {
                string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                int rating = await _context.ProductRatings.Where(x => x.UserId == userId && x.ProductId == id)
                    .Select(x => x.Rating).FirstOrDefaultAsync();
                ViewBag.Rating = rating == 0 ? 5 : rating; 
            }
            return View(data); 
        }
        public async Task<IActionResult> Rating(int productId, int rating)
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var data = await _context.ProductRatings.Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync();
            if(data == null)
            {
                await _context.ProductRatings.AddAsync(new Models.ProductRating
                {
                    UserId = userId,
                    ProductId = productId,
                    Rating = rating
                });
            }
            else
            {
                data.Rating = rating; 
            }
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Details), new {Id = productId }); 
        }
    }
}
