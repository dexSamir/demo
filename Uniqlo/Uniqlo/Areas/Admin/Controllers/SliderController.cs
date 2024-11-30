using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniqlo.DataAccess;
using Uniqlo.Models;
using Uniqlo.ViewModel.Slider;

namespace Uniqlo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        readonly UniqloDbContext _context;
        readonly IWebHostEnvironment _env;

        public SliderController(UniqloDbContext context, IWebHostEnvironment env)
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
            if (!vm.File.ContentType.StartsWith("image"))
                ModelState.AddModelError("File", "File type must be image");
            if (vm.File.Length > 2 * 1024 * 1024)
                ModelState.AddModelError("File", "File length must be less than 2Mb");
            if (!ModelState.IsValid) return View();

            string newFileName = Path.GetRandomFileName() + Path.GetExtension(vm.File.FileName);

            using (Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "Sliders", newFileName)))
            {
                await vm.File.CopyToAsync(stream);
            }

            Slider slider = new Slider
            {
                ImageUrl = newFileName,
                Title = vm.Title,
                Subtitle = vm.Subtitle,
                Link = vm.Link
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if(data is null)
            {
                return NotFound(); 
            }
            _context.Sliders.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        //public async Task<IActionResult> Update(int? id, SliderCreateVM vm)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    if(!vm.File.ContentType.StartsWith("image"))
        //    {
        //        ModelState.AddModelError("File", "file must be an image");
        //        return View(); 
        //    }
        //}
    }
}
