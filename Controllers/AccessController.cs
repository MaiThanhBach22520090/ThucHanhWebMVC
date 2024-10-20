using Microsoft.AspNetCore.Mvc;
using ThucHanhWebMVC.Models;

namespace ThucHanhWebMVC.Controllers
{
	public class AccessController : Controller
	{
		QlbanVaLiContext db = new QlbanVaLiContext();

		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetString("Username") != null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public IActionResult Login(TUser user)
		{
			if (HttpContext.Session.GetString("Username") == null)
			{
				var u = db.TUsers.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
				if (u != null)
				{
					HttpContext.Session.SetString("Username", u.Username.ToString());
					return RedirectToAction("Index", "Home");	
				}
			}

			return View();
		}
	}
}
