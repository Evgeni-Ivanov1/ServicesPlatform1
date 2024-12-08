using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Category;
using ServicesPlatform.Models.OutputModels.Category;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesPlatform.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryDetailsOutputModel> CreateAsync(CreateCategoryInputModel model)
        {
            var entityModel = new Category
            {
                Name = model.Name,
                Description = model.Description
            };

            var addedCategory = await this.categoryRepository.CreateAsync(entityModel);

            return new CategoryDetailsOutputModel
            {
                Name = addedCategory.Name,
                Description = addedCategory.Description,
                Id = addedCategory.Id
            };
        }

        public async Task<List<CategorySimplifiedModel>> GetAllAsync()
        {
            var allCategories = await this.categoryRepository.GetAllAsync();

            var outputModels = allCategories
                .Select(c => new CategorySimplifiedModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                .ToList();

            return outputModels;
        }

        public async Task<CategoryDetailsOutputModel> UpdateAsync(UpdateCategoryInputModel model)
        {
            var category = await this.categoryRepository.GetByIdAsync(model.Id);

            category.Name = model.Name;
            category.Description = model.Description;

            var updatedCategory = await this.categoryRepository.UpdateAsync(category);

            return new CategoryDetailsOutputModel
            {
                Name = updatedCategory.Name,
                Description = updatedCategory.Description,
                Id = updatedCategory.Id
            };
        }

        public async Task DeleteAsync(int id)
        {
            var category = await this.categoryRepository.GetByIdAsync(id);

            await this.categoryRepository.DeleteAsync(category);
        }

    }
}
