using Microsoft.AspNetCore.Mvc;
using ReservationPlatform.Data;

namespace ReservationPlatform.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int serviceId)
        {
            // Логика за създаване на ревю
            return View();
        }
    }
}
