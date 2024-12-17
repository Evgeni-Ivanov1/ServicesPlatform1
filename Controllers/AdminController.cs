using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Data.Models;
using System; 
using System.Collections.Generic;

namespace ReservationPlatform.Controllers
{
    public class AdminController : Controller
    {
     
        public IActionResult Dashboard()
        {
            return View();
        }


        public IActionResult Logs()
        {
     
            var logs = new List<AdminLog>
            {
                new AdminLog { Id = 1, Action = "Added new service", PerformedBy = "Admin", PerformedOn = DateTime.Now.AddDays(-1) },
                new AdminLog { Id = 2, Action = "Deleted a reservation", PerformedBy = "Admin", PerformedOn = DateTime.Now.AddHours(-5) },
                new AdminLog { Id = 3, Action = "Updated service details", PerformedBy = "Admin", PerformedOn = DateTime.Now }
            };

            return View(logs);
        }
    }
}
