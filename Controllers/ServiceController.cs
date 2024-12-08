using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationPlatform.Controllers
{
    public class ServiceController : Controller
    {
        private readonly List<Service> _services = new List<Service>
        {
            new Service
            {
                Id = 1,
                Name = "Web Development",
                Description = "Create modern websites.",
                Price = 1200,
                Category = "IT",
                ImageUrl = "/images/webdev.jpg",
                Availability = "Available",
                CreatedOn = DateTime.Now.AddDays(-30)
            },
            new Service
            {
                Id = 2,
                Name = "Graphic Design",
                Description = "Design stunning visuals.",
                Price = 800,
                Category = "Design",
                ImageUrl = "/images/graphic.jpg",
                Availability = "Available",
                CreatedOn = DateTime.Now.AddDays(-20)
            },
            new Service
            {
                Id = 3,
                Name = "SEO Optimization",
                Description = "Improve website visibility.",
                Price = 600,
                Category = "Marketing",
                ImageUrl = "/images/seo.jpg",
                Availability = "Available",
                CreatedOn = DateTime.Now.AddDays(-10)
            } ,
    new Service
    {
        Id = 4,
        Name = "Relaxing Massage",
        Description = "Marty-Party is back again. Relax in my hands; I offer the best Lumi-Lumi massage. Additional services are available for an extra fee, slide is included in the price.",
        Price = 300,
        Category = "Relax",
        ImageUrl = "/images/massage.jpg",
        Availability = "Available",
        CreatedOn = DateTime.Now
    }
};

        private static readonly List<Review> _reviews = new List<Review>
        {
            new Review { Id = 1, ServiceId = 1, Username = "John Doe", Comment = "Great service!", Rating = 5 },
            new Review { Id = 2, ServiceId = 1, Username = "Jane Smith", Comment = "Very satisfied!", Rating = 4 },
            new Review { Id = 1, ServiceId = 4, Username = "Rumen Tsonkov aka eblio", Comment = "Correct! Recommend..", Rating = 5 }
        };

        private static readonly List<Reservation> _reservations = new List<Reservation>();

        public IActionResult Index()
        {
            return View(_services);
        }

        public IActionResult Details(int id)
        {
            var service = _services.FirstOrDefault(s => s.Id == id);
            if (service == null) return NotFound();

            ViewBag.Reviews = _reviews.Where(r => r.ServiceId == id).ToList();
            ViewBag.Reservations = _reservations.Where(r => r.ServiceId == id).ToList();

            return View(service);
        }

        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            if (ModelState.IsValid)
            {
                review.Id = _reviews.Max(r => r.Id) + 1;
                _reviews.Add(review);
                return RedirectToAction("Details", new { id = review.ServiceId });
            }

            return RedirectToAction("Details", new { id = review.ServiceId });
        }

        [HttpPost]
        public IActionResult CreateReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var service = _services.FirstOrDefault(s => s.Id == reservation.ServiceId);
                if (service != null)
                {
                    reservation.Id = _reservations.Count + 1;
                    reservation.ServiceName = service.Name;
                    reservation.Price = service.Price;
                    _reservations.Add(reservation);
                }
                return RedirectToAction("Details", new { id = reservation.ServiceId });
            }

            return RedirectToAction("Details", new { id = reservation.ServiceId });
        }
    }
}
