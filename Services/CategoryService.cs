using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Category;
using ServicesPlatform.Models.OutputModels.Category;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesPlatform.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDetailsOutputModel>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDetailsOutputModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();
        }

        public async Task<CategoryDetailsOutputModel> CreateAsync(CreateCategoryInputModel model)
        {
            var category = new Category
            {
                Name = model.Name,
                Description = model.Description
            };

            var addedCategory = await _categoryRepository.CreateAsync(category);

            return new CategoryDetailsOutputModel
            {
                Id = addedCategory.Id,
                Name = addedCategory.Name,
                Description = addedCategory.Description
            };
        }

        public async Task<CategoryDetailsOutputModel> UpdateAsync(UpdateCategoryInputModel model)
        {
            var category = await _categoryRepository.GetByIdAsync(model.Id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            category.Name = model.Name;
            category.Description = model.Description;

            var updatedCategory = await _categoryRepository.UpdateAsync(category);

            return new CategoryDetailsOutputModel
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                Description = updatedCategory.Description
            };
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            await _categoryRepository.DeleteAsync(category);
        }
    }
}
