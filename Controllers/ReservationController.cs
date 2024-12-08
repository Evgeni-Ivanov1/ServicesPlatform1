using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationPlatform.Controllers
{
    public class ReservationController : Controller
    {
        private static readonly List<Reservation> _reservations = new List<Reservation>();

        public IActionResult MyReservations()
        {
            return View(_reservations);
        }

        public IActionResult Create(string serviceName, decimal price)
        {
            var model = new Reservation
            {
                ServiceName = serviceName,
                Price = price,
                ReservationDate = DateTime.Now
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Reservation model)
        {
            if (ModelState.IsValid)
            {
                _reservations.Add(model);
                TempData["ReservationSuccess"] = $"Reservation for {model.ServiceName} on {model.ReservationDate.ToShortDateString()} has been successfully created.";
                return RedirectToAction("MyReservations");
            }

            return View(model);
        }

        public IActionResult Cancel(int id)
        {
            var reservation = _reservations.FirstOrDefault(r => r.Id == id);
            if (reservation != null)
            {
                _reservations.Remove(reservation);
                TempData["Message"] = $"Reservation with ID {id} was canceled.";
            }
            return RedirectToAction("MyReservations");
        }
    }
}
