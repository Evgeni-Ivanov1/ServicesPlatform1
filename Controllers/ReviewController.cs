using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Review;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationPlatform.Controllers
{
    [Authorize] 
    public class ReviewController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReviewService _reviewService;

        public ReviewController(UserManager<ApplicationUser> userManager, IReviewService reviewService)
        {
            _userManager = userManager;
            _reviewService = reviewService;
        }

        [AllowAnonymous] 
        public async Task<IActionResult> List(int serviceId)
        {
            var reviews = await _reviewService.GetReviewsByServiceIdAsync(serviceId);
            ViewBag.ServiceId = serviceId;
            return View(reviews);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddReviewInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.Name;

            await _reviewService.AddReviewAsync(new AddReviewInputModel
            {
                ServiceId = model.ServiceId,
                UserName = model.UserName,
                Comment = model.Comment,
                Rating = model.Rating
            }, userId);
     
            return RedirectToAction("Details", "Service", new { id = model.ServiceId });
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int reviewId)
        {
            var review = await _reviewService.GetReviewByIdAsync(reviewId);
            if (review == null) return NotFound();

            var model = new AddReviewInputModel
            {
                ServiceId = review.ServiceId,
                UserName = review.UserName,
                Comment = review.Comment,
                Rating = review.Rating
            };
            return View("Details", model); 
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int reviewId, AddReviewInputModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _reviewService.UpdateReviewAsync(reviewId, model.Comment, model.Rating);

            return RedirectToAction("Details", "Service", new { id = model.ServiceId });
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var user = await _userManager.GetUserAsync(User);
            var review = await _reviewService.GetReviewByIdAsync(reviewId);

            if (review == null) return NotFound();

            if (review.UserId != user.Id && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            await _reviewService.DeleteReviewAsync(reviewId);
            return RedirectToAction("Details", "Service", new { id = review.ServiceId });
        }

    }
}
