using System;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Hi(string name, int age)
		{
			//JsonResult json = new JsonResult(new
			//{
			//	name = name,
			//	age = age
			//});
			//ContentResult content = new ContentResult();
			//content.Content = "<h1>Samir</h1>";
			//content.ContentType = "text/html";

			//ViewResult view = new ViewResult();
			List<string> names = new List<string>{ "Mirmezahir", "Kenan", "Xaqan", "Emin", "Xalid", "Xendam" };
			ViewData["students"] = names;
			return View(); 
		}

        public int Vay(int id)
        {
            return id;
        }
    }
}

