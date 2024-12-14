using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Review;
using System.Threading.Tasks;

namespace ServicesPlatform.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task AddReviewAsync(AddReviewInputModel dto, string userId)
        {
            var review = new Review
            {
                ServiceId = dto.ServiceId,
                Comment = dto.Comment,
                Rating = dto.Rating,
                UserId = userId
            };

            await _reviewRepository.AddReviewAsync(review);

            await _reviewRepository.SaveChangesAsync();
        }
    }
}