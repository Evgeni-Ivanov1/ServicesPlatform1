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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddReviewInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Service", new { id = model.ServiceId });
            }

            // Извличаме UserId от текущия потребител
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); // Ако потребителят не е логнат
            }

            // Добавяме ревю
            await _reviewService.AddReviewAsync(model, userId);

            return RedirectToAction("Details", "Service", new { id = model.ServiceId });
        }





        [HttpGet]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int reviewId, string comment, int rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = await _reviewService.GetReviewByIdAsync(reviewId);
            if (review == null) return NotFound();

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (review.UserId != userId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            await _reviewService.UpdateReviewAsync(reviewId, comment, rating);

            return RedirectToAction("Details", "Service", new { id = review.ServiceId });
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
