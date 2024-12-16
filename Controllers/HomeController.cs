using Microsoft.AspNetCore.Mvc;

namespace ReservationPlatform.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult CookiesPolicy()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }

    }
}
