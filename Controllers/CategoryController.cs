using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Models;
using ServicesPlatform.Models.InputModels.Category;
using ServicesPlatform.Models.OutputModels.Category;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationPlatform.Controllers
{
    [Authorize(Roles = "Administrator")]
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _categoryService.CreateAsync(model);       
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }

            await _categoryService.UpdateAsync(model);
            return Json(new { success = true});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
