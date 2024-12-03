using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniqloProject.Models;
using UniqloProject.ViewModel.Auths;

namespace UniqloProject.Controllers
{
    public class AccountController(UserManager<User> _userManager) : Controller
    {
        // GET: AccountController
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserCreateVM vm )
        {
            if (!ModelState.IsValid)
                return View(vm);
            User user = new User
            {
                Email = vm.Email,
                Username = vm.Username,
                Fullname = vm.Fullname,
                ProfileImageUrl = "photo.jpeg"
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(); 
            }
            return View(); 
        }

    }
}
