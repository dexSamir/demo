using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqlo.DataAccess;
using Uniqlo.ViewModel.Common;
using Uniqlo.ViewModel.Product;
using Uniqlo.ViewModel.Slider;

namespace Uniqlo.Controllers
{
    public class HomeController : Controller
    {
        readonly UniqloDbContext _context;
        public HomeController(UniqloDbContext context)
        {
            _context = context; 
        }
        public async Task<IActionResult> Index()
        {
            HomeVm vm = new(); 
            vm.Sliders = await _context.Sliders
                .Where(x=> !x.isDeleted)
                .Select(x => new SliderItemVM
            {
                Title = x.Title,
                Subtitle = x.Subtitle,
                Link = x.Link,
                ImageUrl = x.ImageUrl
            }).ToListAsync();
            vm.Products = await _context.Products
                .Where(x => !x.isDeleted)
                .Select(x => new ProductItemVM
                {
                    Discount = x.Discount,
                    Id = x.Id,
                    ImageUrl = x.CoverImage,
                    IsInStcok = x.Quantity > 0,
                    Name = x.Name,
                    Price = x.Sellprice,
                }).ToListAsync(); 
            return View(vm);
        }
        public ActionResult About()
        {
            return View();
        }

    }
}
