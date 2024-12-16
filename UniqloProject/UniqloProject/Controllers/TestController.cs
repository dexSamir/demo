using Microsoft.AspNetCore.Mvc;

namespace UniqloProject.Controllers
{
    public class TestController : Controller
    {
        // GET: TestController
        public ActionResult SetSession(string key, string value )
        {
            HttpContext.Session.SetString(key, value); 
            return Ok();
        }
        public async Task<IActionResult> GetSession(string key)
        {
            HttpContext.Session.Remove(key); 
            return Content(HttpContext.Session.GetString(key)); 
        }
        public async Task<IActionResult> SetCookie(string key, string value)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(1)
            }) ;
            return View(); 
        }
        public async Task<IActionResult> GetCookie(string key)
        {
            return Content(HttpContext.Request.Cookies[key]); 
        }
    }
}
