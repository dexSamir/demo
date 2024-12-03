using Microsoft.AspNetCore.Mvc;

namespace UniqloProject.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: DashboardController
        [Area("Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
