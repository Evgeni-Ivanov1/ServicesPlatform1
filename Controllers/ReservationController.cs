using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Models.InputModels.Reservation;
using System.Threading.Tasks;

namespace ReservationPlatform.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        public async Task<IActionResult> MyReservations()
        {
            var reservations = await _service.GetAllAsync();
            return View(reservations);
        }

        [HttpGet]
        public IActionResult Create(string serviceName, decimal price)
        {
            var model = new CreateReservationInputModel
            {
                ServiceName = serviceName,
                Price = price,
                ReservationDate = System.DateTime.Now
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationInputModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(model);
                TempData["Message"] = $"Reservation for {model.ServiceName} was successfully created.";
                return RedirectToAction(nameof(MyReservations));
            }
            return View(model);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await _service.DeleteAsync(id);
            TempData["Message"] = $"Reservation was successfully canceled.";
            return RedirectToAction(nameof(MyReservations));
        }
    }
}
