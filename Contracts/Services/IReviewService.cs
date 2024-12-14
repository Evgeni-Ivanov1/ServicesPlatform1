using ServicesPlatform.Models.InputModels.Review;
using System.Threading.Tasks;

namespace ServicesPlatform.Contracts.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(AddReviewInputModel dto, string userId);
    }

}
