using Microsoft.AspNetCore.Mvc;
using ReservationPlatform.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReservationPlatform.Controllers
{
    public class ServiceController : Controller
    {
        // ???????? ?????? ? ??????
        private readonly List<Service> _services = new List<Service>
        {
            new Service { Id = 1, Name = "Web Development", Description = "Create modern websites.", Price = 1200 },
            new Service { Id = 2, Name = "Graphic Design", Description = "Design stunning visuals.", Price = 800 },
            new Service { Id = 3, Name = "SEO Optimization", Description = "Improve website visibility.", Price = 600 }
        };

        public IActionResult Index()
        {
            return View(_services);
        }

        public IActionResult Details(int id)
        {
            var service = _services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }
    }
}
