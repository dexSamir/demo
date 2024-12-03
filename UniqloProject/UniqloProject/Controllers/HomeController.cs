using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniqloProject.DataAccess;
using UniqloProject.ViewModel.Common;
using UniqloProject.ViewModel.Product;
using UniqloProject.ViewModel.Slider;

namespace UniqloProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        readonly UniqloAppDbContext _context;
        public HomeController(UniqloAppDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM();

            vm.Sliders = await _context.Sliders
                .Where(x => !x.isDeleted)
                .Select(x => new SliderItemVM
                {
                    ImageUrl = x.ImageUrl,
                    Link = x.Link,
                    Title = x.Title,
                    Subtitle = x.SubTitle
                }).ToListAsync();

            vm.Products = await _context.Products
                .Where(x => !x.isDeleted)
                .Select(x => new ProductItemVM
                {
                    Discount = x.Discount,
                    ImageUrl = x.CoverImage,
                    IsInStock = x.Quantity > 0,
                    Name = x.Name,
                    Price = x.SellPrice
                }).ToListAsync();

            return View(vm);
        }
    }
}
