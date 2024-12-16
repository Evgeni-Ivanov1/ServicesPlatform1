using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Models.InputModels.Service;
using ServicesPlatform.Services;
using System.Linq;
using System.Threading.Tasks;

public class ServiceController : Controller
{
    private readonly IServiceService _serviceService;
    private readonly ICategoryService _categoryService;


    public ServiceController(IServiceService serviceService, ICategoryService categoryService)
    {
        _serviceService = serviceService;
        _categoryService = categoryService;
    }
  

    public async Task<IActionResult> Index()
    {
        var services = await _serviceService.GetAllAsync();
        return View(services);
    }
 public async Task<IActionResult> Details(int id)
{
    var service = await _serviceService.GetByIdWithReviewsAsync(id);

    if (service == null)
    {
        return NotFound();
    }

    return View(service);
}


    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var categories = await _categoryService.GetAllAsync();
        var viewModel = new CreateServiceInputModel
        {
            Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateServiceInputModel model)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllAsync();
            model.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(model);
        }

        await _serviceService.CreateAsync(model);
        TempData["SuccessMessage"] = "Service added successfully.";
        return RedirectToAction(nameof(Index));
    }
 

}
