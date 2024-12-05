using Microsoft.AspNetCore.Mvc;
using ReservationPlatform.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReservationPlatform.Controllers
{
    public class ReviewController : Controller
    {
        private static readonly List<Review> _reviews = new List<Review>
        {
            new Review { Id = 1, ServiceId = 1, Username = "John Doe", Comment = "Great service!", Rating = 5 },
            new Review { Id = 2, ServiceId = 1, Username = "Jane Smith", Comment = "Very satisfied!", Rating = 4 }
        };

        public IActionResult List(int serviceId)
        {
            var serviceReviews = _reviews.Where(r => r.ServiceId == serviceId).ToList();
            ViewBag.ServiceId = serviceId;
            return View(serviceReviews);
        }

        public IActionResult Create(int serviceId)
        {
            var model = new Review { ServiceId = serviceId };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Review model)
        {
            if (ModelState.IsValid)
            {
                model.Id = _reviews.Max(r => r.Id) + 1;
                _reviews.Add(model);
                return RedirectToAction("List", new { serviceId = model.ServiceId });
            }
            return View(model);
        }
    }
}
