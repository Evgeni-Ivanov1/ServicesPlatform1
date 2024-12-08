using Microsoft.AspNetCore.Mvc;
using ReservationPlatform.Data;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Models.InputModels.Category;
using ServicesPlatform.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationPlatform.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
            var addedCategory = await _categoryService.CreateAsync(model);

            return View(addedCategory);
        }

        public async Task<IActionResult> Edit(UpdateCategoryInputModel model)
        {
            var categories = await _categoryService.UpdateAsync(model);

            return View(categories);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);

            return View();
        }
    }
}
