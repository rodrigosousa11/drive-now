using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
