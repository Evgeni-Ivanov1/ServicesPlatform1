using ServicesPlatform.Data.Models;
using System.Threading.Tasks;

namespace ServicesPlatform.Contracts.Repositories
{
    public interface IReviewRepository
    {
        Task AddReviewAsync(Review review);
        
        Task SaveChangesAsync();
    }
}
