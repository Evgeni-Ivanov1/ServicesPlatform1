using Microsoft.EntityFrameworkCore;
using ReservationPlatform.Data;
using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetByServiceIdAsync(int serviceId)
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Where(r => r.ServiceId == serviceId)
            .ToListAsync();
    }

    public async Task AddReviewAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task<Review> GetByIdAsync(int reviewId)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == reviewId);
    }

    public async Task UpdateReviewAsync(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(int reviewId)
    {
        var review = await GetByIdAsync(reviewId);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
