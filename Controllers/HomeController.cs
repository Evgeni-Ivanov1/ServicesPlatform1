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
        [Route("Test/ErrorCode")]
        ///Test/ErrorCode?statusCode=500
        public IActionResult ErrorCode(int statusCode)
        {
            return StatusCode(statusCode, $"This is a test for status code {statusCode}.");
        }


    }
}