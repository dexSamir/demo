using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Versioning;
using UniqloProject.DataAccess;
using UniqloProject.Models;
using UniqloProject.ViewModel.Basket;
using UniqloProject.ViewModel.Product;
using Prod = UniqloProject.ViewModel.Product;

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
        public async Task<ActionResult> Index()
        {
            IQueryable<Product> query = _context.Products.Where(x => !x.isDeleted);
            ProductIndexVM vm = new ProductIndexVM
            {
                Products = await query.Select(x => new Prod.ProductItemVM
                {
                    Id = x.Id,
                    IsInStock = x.Quantity > 0,
                    Discount = x.Discount,
                    Name = x.Name,
                    ImageUrl = x.CoverImage,
                    Price = x.SellPrice
                }).ToListAsync(),
                Categories = [new CategoryAndCount { Id = 0, Count = await query.CountAsync(), Name = "All" }]
            };
            vm.Categories.AddRange(ViewBag.Categories = await _context.Categories.Where(x => !x.isDeleted).Select(x => new
            CategoryAndCount{
                Name = x.Name,
                Id = x.Id,
                Count = x.Products.Count()
            }).ToListAsync()); 
            return View(vm);
        }

        public async Task<IActionResult> Filter(int? catId = 0, string? price = null)
        {
            if (!catId.HasValue) return BadRequest();
            var query = _context.Products.Where(x => !x.isDeleted);
            if(catId != 0)
            {
                query = query.Where(x => x.CategoryId == catId); 
            }
            return PartialView("_ProductPartial", await query.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Products
                .Where(x => x.Id == id)
                .Include(x => x.Images)
                .Include(x => x.Ratings)
                .Include(x => x.Comments)
                    .ThenInclude(y => y.User)
                .FirstOrDefaultAsync();

            if (data == null) return NotFound();

            ViewBag.Rating = 5; 
            if(User.Identity?.IsAuthenticated ?? false)
            {
                string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
                int rating = await _context.ProductRatings.Where(x => x.UserId == userId && x.ProductId == id)
                    .Select(x => x.Rating).FirstOrDefaultAsync();
                ViewBag.Rating = rating == 0 ? 5 : rating; 
            }
            return View(data); 
        }
        public async Task<IActionResult> Rating(int productId, int rating)
        {
            string userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
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
        public async Task<IActionResult> Comment(int productId, CommentCreateVM vm)
        {
            string UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var data = await _context.Comments.Where(x => x.ProductId == productId && x.UserId == UserId).FirstOrDefaultAsync();
            Comment comment = new Comment
            {
                UserId = UserId,
                ProductId = productId,
                Content = vm.Content
  
            };
            await _context.Comments.AddAsync(comment); 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = productId}); 
        }

        public async Task<IActionResult> AddBasket(int id )
        {
            if(!await _context.Products.AnyAsync(x=> x.Id == id))
                return NotFound();
            var basketItems = JsonSerializer.Deserialize<List<BasketProductItemVM>>(Request.Cookies["basket"] ?? "[]");
            var item = basketItems?.FirstOrDefault(x => x.Id == id);
            if(item == null)
            {
                item = new BasketProductItemVM
                {
                    Id = id,
                    Count = 0
                };
                basketItems.Add(item); 
            }
            item.Count++; 
            Response.Cookies.Append("basket", JsonSerializer.Serialize(basketItems));
            return Ok(); 
        }
    }
}
