using Microsoft.AspNetCore.Mvc;

namespace Allup.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
