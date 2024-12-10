using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniqloProject.DataAccess;
using UniqloProject.Extension;
using UniqloProject.Helpers;
using UniqloProject.Models;
using UniqloProject.ViewModel.Slider;

namespace UniqloProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Slider)]
    public class SliderController : Controller
    {
        readonly UniqloAppDbContext _context;
        readonly IWebHostEnvironment _env;

        public SliderController(IWebHostEnvironment env, UniqloAppDbContext context)
        {
            _context = context;
            _env = env; 
        }


        // GET: SliderController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if(!vm.File.IsValidType("image"))
            {
                ModelState.AddModelError("File", "File type must be an image");
                return View(); 
            }
            if(!vm.File.IsValidSize(300))
            {
                ModelState.AddModelError("File", "File size must be less than 300kb");
                return View(); 
            }
            string fileName = await vm.File.UploadAsync(_env.WebRootPath, "imgs", "sliders");
            Slider slider = new Slider
            {
                ImageUrl = fileName,
                Title = vm.Title,
                SubTitle = vm.Subtitle,
                Link = vm.Link
            };
            await _context.AddAsync(slider); 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));   
        }


        //Delete


        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return View();
            string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imgs", "sliders", data.ImageUrl);
            if(System.IO.Directory.Exists(oldPath))
            {
                System.IO.Directory.Delete(oldPath); 
            }
            _context.Sliders.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var slider = await _context.Sliders
                .Where(c => c.Id == id)
                .Select(x => new UpdateSliderVM
                {
                    Link = x.Link,
                    Title = x.Title,
                    SubTitle = x.SubTitle,
                    ExistingImageUrl = x.ImageUrl
                }).FirstOrDefaultAsync();
            if (slider == null) return NotFound();
            return View(slider); 
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSliderVM vm)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            if (!ModelState.IsValid) return View(vm);

            if (vm.File != null)
            {
                if (!vm.File.IsValidType("image"))
                {
                    ModelState.AddModelError("File", "File type must be an image");
                    return View(vm);
                }
                if (!vm.File.IsValidSize(300))
                {
                    ModelState.AddModelError("File", "File must be less than 300kb");
                    return View(vm);
                }

                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), _env.WebRootPath, "imgs", "sliders", data.ImageUrl);

                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);

                string newFileName = await vm.File.UploadAsync(_env.WebRootPath, "imgs", "sliders");
                data.ImageUrl = newFileName; 
            }
            data.Link = vm.Link;
            data.Title = vm.Title;
            data.SubTitle = vm.SubTitle;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }

        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return NotFound(); ;

            data.isDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return NotFound();;

            data.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
    }
}
