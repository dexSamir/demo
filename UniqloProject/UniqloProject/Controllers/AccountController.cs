using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;
using UniqloProject.Enums;
using UniqloProject.Helpers;
using UniqloProject.Models;
using UniqloProject.Services.Abstracts;
using UniqloProject.ViewModel.Auths;

namespace UniqloProject.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;
        readonly IEmailService _service;

        bool isAuthenticated => User.Identity?.IsAuthenticated ?? false;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service; 
        }
        public async Task<IActionResult> Register()
        {
            if (isAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreateVM vm)
        {
            if (isAuthenticated) return RedirectToAction("Index", "Home");
            if (!ModelState.IsValid)
                return View();
            User user = new User
            {
                Fullname = vm.Fullname,
                UserName = vm.Username,
                Email = vm.Email,
                ProfileImageUrl = "photo.jpg"
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                 }
                return View();
            }
            var roleResult = await _userManager.AddToRoleAsync(user, nameof(Roles.User));
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            _service.SendEmailConfirmation(user.Email, user.UserName, token);

            return Content("Email sent"); 
        }

        public async Task<IActionResult> Login()
        {
            if (isAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm, string? ReturnUrl = null)
        {
            if (isAuthenticated) return RedirectToAction("Index", "Home");
            if (!ModelState.IsValid) return View();
            User? user = null;
            if (vm.UsernameOrEmail.Contains('@'))
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);

            else
                user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            if (user is null)
            {
                ModelState.AddModelError("", "username or password is wrong");
                return View(); 
            }
            var result = await _signInManager.PasswordSignInAsync(user,vm.Password,  vm.RememberMe, true); 
            if(!result.Succeeded)
            {
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "username or password is wrong");
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "wait until" + user.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                }
                return View(); 
            }
            if(string.IsNullOrEmpty(ReturnUrl))
            {
                if(await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("Index", new {Controller = "Dashboard", Area = "Admin"});
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return LocalRedirect(ReturnUrl); 
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Send()
        {
            return Ok(); 
        }

        public async Task<IActionResult> VerifyEmail(string token, string user )
        {
            var entity = await _userManager.FindByNameAsync(user);
            if (entity is null) return BadRequest(); 
            var result = await _userManager.ConfirmEmailAsync(entity, token.Replace(' ', '+'));
            if(!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                
                foreach (var item in result.Errors)
                {
                    sb.AppendLine(item.Description); 
                }
                return Content(sb.ToString()); 
            }
            await _signInManager.SignInAsync(entity, true);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgetPassword()
        {
            if (isAuthenticated) return RedirectToAction("Index", "Home");
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM VM)
        {
            string email = VM.UsernameOrEmail;
            if (email is null)
            {
                ModelState.AddModelError("", "Email is required");
                return View(); 
            }
            User user = email.Contains('@') ?await _userManager.FindByEmailAsync(email) :await _userManager.FindByNameAsync(email); 
            if(user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(); 
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);
            _service.SendEmailConfirmation(user.Email, user.UserName, token);
            return Content("Password reset link sent to your email"); 
        }
        public IActionResult ResetPassword (string token , string email)
        {
            if (email is null) return BadRequest();
            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model); 
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email.");
                return View();
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.newPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            return RedirectToAction(nameof(Login));
        }
    }
}
