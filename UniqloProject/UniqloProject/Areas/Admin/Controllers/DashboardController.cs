using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniqloProject.Enums;
using UniqloProject.Helpers;

namespace UniqloProject.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: DashboardController
        [Area("Admin")]
        [Authorize(Roles = RoleConstants.Dashboard)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
