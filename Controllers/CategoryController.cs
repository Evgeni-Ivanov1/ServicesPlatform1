using Microsoft.AspNetCore.Mvc;
using ReservationPlatform.Data;
using System.Linq;

namespace ReservationPlatform.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
    }
}
