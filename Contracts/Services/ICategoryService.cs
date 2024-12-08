using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Models.InputModels.Category;
using ServicesPlatform.Models.OutputModels.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesPlatform.Contracts.Services
{
    public interface ICategoryService
    {
        Task<List<CategorySimplifiedModel>> GetAllAsync();
        Task<CategoryDetailsOutputModel> CreateAsync(CreateCategoryInputModel model);
        Task<CategoryDetailsOutputModel> UpdateAsync(UpdateCategoryInputModel model);
        Task DeleteAsync(int id);
    }
}
