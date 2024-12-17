using System.Collections.Generic;
using System.Threading.Tasks;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Review;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetReviewsByServiceIdAsync(int serviceId);
    Task<Review> GetReviewByIdAsync(int reviewId);
    Task AddReviewAsync(AddReviewInputModel dto, string userId);
    Task UpdateReviewAsync(int reviewId, string comment, int rating);
    Task DeleteReviewAsync(int reviewId);
}
