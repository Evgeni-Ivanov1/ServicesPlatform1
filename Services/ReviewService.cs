using Azure.Identity;
using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Review;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Review>> GetReviewsByServiceIdAsync(int serviceId)
        {
            return await _reviewRepository.GetByServiceIdAsync(serviceId);
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await _reviewRepository.GetByIdAsync(reviewId);
        }

        public async Task AddReviewAsync(AddReviewInputModel dto, string userId)
        {
            var review = new Review
            {
                ServiceId = dto.ServiceId,
                UserId = userId,
                UserName = dto.UserName,
                Comment = dto.Comment,
                Rating = dto.Rating
            };

            await _reviewRepository.AddReviewAsync(review);
        }


        public async Task UpdateReviewAsync(int reviewId, string comment, int rating)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new ArgumentException("Comment cannot be empty.", nameof(comment));
            }

            var review = await _reviewRepository.GetByIdAsync(reviewId);

            if (review != null)
            {
                review.Comment = comment;
                review.Rating = rating;
                await _reviewRepository.UpdateReviewAsync(review);
            }
        }


        public async Task DeleteReviewAsync(int reviewId)
        {
            await _reviewRepository.DeleteReviewAsync(reviewId);
        }

    }
}
