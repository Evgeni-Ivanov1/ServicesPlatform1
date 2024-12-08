using ServicesPlatform.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesPlatform.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(int categoryId);

        Task<Category> CreateAsync(Category category);

        Task<Category> UpdateAsync(Category category);

        Task DeleteAsync(Category id);
    }
}
