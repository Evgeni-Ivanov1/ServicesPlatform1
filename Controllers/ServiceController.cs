using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Models.InputModels.Service;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ServicesPlatform.Models.InputModels; 
using ServicesPlatform.Data.Models;


[Authorize] 
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

    // GET: Add Service (???????? ???? ?? ???????????? ??????????? ? ??????????????)
    [HttpGet]
    [Authorize]
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
    [Authorize]
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

        model.OwnerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await _serviceService.CreateAsync(model);
        TempData["SuccessMessage"] = "Service added successfully.";
        return RedirectToAction(nameof(Index));
    }


    // GET: Edit Service
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var service = await _serviceService.GetByIdAsync(id);
        if (service == null)
        {
            return NotFound();
        }

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (service.OwnerId != userId && !User.IsInRole("Administrator"))
        {
            return Forbid();
        }

        var model = new UpdateServiceInputModel
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price,
            CategoryId = service.CategoryId,
            ImageUrl = service.ImageUrl,
            Availability = service.Availability
        };

        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(UpdateServiceInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var service = await _serviceService.GetByIdAsync(model.Id);
        if (service == null) return NotFound();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (service.OwnerId != userId && !User.IsInRole("Administrator"))
        {
            return Forbid();
        }

        await _serviceService.UpdateAsync(model);
        TempData["SuccessMessage"] = "Service updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    // POST: Delete Service
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var service = await _serviceService.GetByIdAsync(id);
        if (service == null)
        {
            return NotFound();
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (service.OwnerId != userId && !User.IsInRole("Administrator"))
        {
            return Forbid();
        }

        await _serviceService.DeleteAsync(id);
        TempData["SuccessMessage"] = "Service deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
    [Authorize] 
    public async Task<IActionResult> MyServices()
    {
        
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(); 
        }

     
        var userServices = await _serviceService.GetServicesByOwnerIdAsync(userId);

        return View("Index", userServices); 
    }



}
