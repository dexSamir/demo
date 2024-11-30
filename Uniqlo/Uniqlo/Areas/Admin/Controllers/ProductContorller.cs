using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Uniqlo.DataAccess;
using Uniqlo.Extensions;
using Uniqlo.Models;
using Uniqlo.ViewModel.Product;

namespace Uniqlo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductContorller : Controller
    {
        // GET: ProductContorller
        readonly IWebHostEnvironment _env;
        readonly UniqloDbContext _context; 
        public ProductContorller(IWebHostEnvironment env, UniqloDbContext context)
        {
            _env = env;
            _context = context; 
        }
        public async Task<IActionResult> Index()
        { 
            return View(await _context.Products.Include(x=> x.Category).ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(x => !x.isDeleted).ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if(vm.OtherImages != null && vm.OtherImages.Any())
            {
                if(!vm.OtherImages.All(x => x.IsValidType("image")))
                {
                    var FileNames = vm.OtherImages.Where(x => !x.IsValidType("image")).Select(x => x.FileName);
                    ModelState.AddModelError("OtherImages", string.Join(", ", FileNames) + " are(is) not an image"); 

                }
                if(!vm.OtherImages.All(x=> x.isValidSize(300)))
                {
                    var FileNames = vm.OtherImages.Where(x => !x.isValidSize(300)).Select(x => x.FileName);
                    ModelState.AddModelError("OtherImages", string.Join(", ", FileNames) + "must be less than 300kb."); 
                }
            }
            if(vm.CoverImage != null)
            {
                if (!vm.CoverImage.IsValidType("image"))
                    ModelState.AddModelError("CoverImage", "File type must be an image");
                if (!vm.CoverImage.isValidSize(300))
                    ModelState.AddModelError("CoverImage", "File must be less than 300 kb"); 
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.Where(x => !x.isDeleted).ToListAsync();
                return View();
            }
            Product product = new Product
            {
                CategoryId = vm.CategoryId,
                CostPrice = vm.CostPrice,
                Description = vm.Description,
                Name = vm.Name,
                Quantity = vm.Quantity,
                Sellprice = vm.Sellprice,
                Discount = vm.Discount,
                CoverImage = await vm.CoverImage!.UploadAsync(_env.WebRootPath, "imgs", "Sliders"),
                Images = vm.OtherImages.Select(x => new ProductImage
                {
                    FileUrl = x.UploadAsync(_env.WebRootPath, "imgs", "products").Result
                }).ToList()
            };
            List<ProductImage> list = new();
            foreach(var item in vm.OtherImages)
            {
                string Filename = await item.UploadAsync(_env.WebRootPath, "imgs", "products");
                list.Add(new ProductImage
                {
                    FileUrl = Filename,
                    Product = product, 
                }); 
            }
            await _context.ProductImages.AddRangeAsync(list); 
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index)); 
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var product = await _context.Products
                .Where(x => x.Id == id.Value)
                .Select(x => new ProductUpdateVM
                {
                    CategoryId = x.CategoryId,
                    CostPrice = x.CostPrice,
                    Sellprice = x.Sellprice,
                    CoverImageUrl = x.CoverImage,
                    Discount = x.Discount,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    OtherImagesUrls = x.Images.Select(y => y.FileUrl)
                }).FirstOrDefaultAsync();
            return View(product); 
        }
    }
}
