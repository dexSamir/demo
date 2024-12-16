using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using UniqloProject.DataAccess;
using UniqloProject.Extension;
using UniqloProject.Helpers;
using UniqloProject.Models;
using UniqloProject.ViewModel.Product;

namespace UniqloProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Product)]
    public class ProductController : Controller
    {
        readonly UniqloAppDbContext _context;
        readonly IWebHostEnvironment _env;
        public ProductController(UniqloAppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env; 
        }
        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.Include(c => c.Category).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(x => !x.isDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if(vm.OtherFiles != null && vm.OtherFiles.Any())
            {
                if(!vm.OtherFiles.All(x => x.IsValidType("image")))
                {
                    var FileNames = vm.OtherFiles.Where(x => !x.IsValidType("image")).Select(x => x.FileName);
                    ModelState.AddModelError("OtherFiles", string.Join(", ", FileNames) + " are(is) not an image"); 
                }
                if(!vm.OtherFiles.All(x => x.IsValidSize(300)))
                {
                    var FileNames = vm.OtherFiles.Where(x => !x.IsValidSize(300)).Select(x => x.FileName);
                    ModelState.AddModelError("OtherFiles", string.Join(", ", FileNames) + "are(is) must be less than 300"); 
                }
            }
            if(vm.CoverFile != null)
            {
                if (!vm.CoverFile.IsValidType("image"))
                    ModelState.AddModelError("CoverFile", "File type must be an image");
                if (!vm.CoverFile.IsValidSize(300))
                    ModelState.AddModelError("CoverFile", "File must be less than 300kb");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.Where(x => !x.isDeleted).ToListAsync(); 
                return View();
            }

            Product product = new Product
            {
                Name = vm.Name,
                CategoryId = vm.CategoryId,
                Description = vm.Description,
                SellPrice = vm.SellPrice,
                CostPrice = vm.CostPrice,
                Quantity = vm.Quantity,
                Discount = vm.Discount,
                CoverImage = await vm.CoverFile!.UploadAsync(_env.WebRootPath, "imgs", "products"),
                Images = vm.OtherFiles.Select(x => new ProductImage
                {
                    FileUrl = x.UploadAsync(_env.WebRootPath, "imgs", "products").Result
                }).ToList()
            };
            //List<ProductImage> list = [];
            //foreach (var item in vm.OtherFiles)
            //{
            //    string fileName = await item.UploadAsync(_env.WebRootPath, "imgs", "products");
            //    list.Add(new ProductImage
            //    {
            //        FileUrl = fileName,
            //        Product = product
            //    }); 
            //}
            //await _context.ProductImages.AddRangeAsync(list); 
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data == null) return NotFound();
            _context.Products.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Products.FindAsync(id);
            if (data == null) return NotFound();
            ProductUpdateVM vm = new();

            vm.Name = data.Name;
            vm.Description = data.Description;
            vm.Discount = data.Discount;
            vm.CostPrice = data.CostPrice;
            vm.SellPrice = data.SellPrice;
            vm.CategoryId = data.CategoryId;
            vm.Quantity = data.Quantity;
            
            ViewBag.Categories = await _context.Categories.Where(x => !x.isDeleted).ToListAsync();
            return View(vm); 
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, ProductUpdateVM vm)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data == null) return NotFound();

            if(vm.CoverFile != null)
            {
                if (!vm.CoverFile.IsValidType("image"))
                {
                    ModelState.AddModelError("CoverFile", "File type must be an image");
                    return View(vm);
                }
                if(!vm.CoverFile.IsValidSize(300))
                {
                    ModelState.AddModelError("CoverFile", "File must be less than 300kb");
                    return View(vm);
                }
                string fileName = Path.Combine(Directory.GetCurrentDirectory(), _env.WebRootPath, "imgs", "products", data.CoverImage); 
                if (System.IO.File.Exists(fileName))
                    System.IO.File.Delete(fileName); 

                string newFileName = await vm.CoverFile!.UploadAsync(_env.WebRootPath, "imgs", "products");
                data.CoverImage = newFileName;
            }
            if (!ModelState.IsValid) return View(vm);

            data.Name = vm.Name;
            data.Description = vm.Description;
            data.CostPrice = vm.CostPrice;
            data.SellPrice = vm.SellPrice;
            data.Quantity = vm.Quantity;
            data.Discount = vm.Discount;
            data.CategoryId = vm.CategoryId;

            ViewBag.Categories = await _context.Products.Where(x => !x.isDeleted).ToListAsync();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 

        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data == null) return NotFound();
            data.isDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Products.FindAsync(id);
            if (data == null) return NotFound();
            data.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
