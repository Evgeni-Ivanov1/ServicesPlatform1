using System.Collections.Generic;
using System.Threading.Tasks;
using ServicesPlatform.Data.Models;

public interface IReviewRepository
{
    Task<List<Review>> GetByServiceIdAsync(int serviceId);
    Task AddReviewAsync(Review review);
    Task<Review> GetByIdAsync(int reviewId);
    Task UpdateReviewAsync(Review review);
    Task DeleteReviewAsync(int reviewId);
}
