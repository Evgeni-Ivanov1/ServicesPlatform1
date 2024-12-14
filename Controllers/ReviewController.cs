using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Review;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationPlatform.Controllers
{
    public class ReviewController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReviewService _reviewService;

        public ReviewController(UserManager<ApplicationUser> userManager, IReviewService reviewService)
        {
            _userManager = userManager;
            _reviewService = reviewService;
        }

        private static readonly List<Review> _reviews = new List<Review>
        {
            //new Review { Id = 1, ServiceId = 1, Username = "John Doe", Comment = "Great service!", Rating = 5 },
            //new Review { Id = 2, ServiceId = 1, Username = "Jane Smith", Comment = "Very satisfied!", Rating = 4 }
        };

        public IActionResult List(int serviceId)
        {
            var serviceReviews = _reviews.Where(r => r.ServiceId == serviceId).ToList();
            ViewBag.ServiceId = serviceId;
            return View(serviceReviews);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] AddReviewInputModel review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            await _reviewService.AddReviewAsync(review, user.Id);

            return RedirectToAction("List", new { serviceId = review.ServiceId });
        }

        public IActionResult Create(int serviceId)
        {
            var model = new Review { ServiceId = serviceId };
            return View(model);
        }

        //[HttpPost]
        //public IActionResult Create(Review model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.Id = _reviews.Max(r => r.Id) + 1;
        //        _reviews.Add(model);
        //        return RedirectToAction("List", new { serviceId = model.ServiceId });
        //    }
        //    return View(model);
        //}
    }
}
