using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Contracts.Services;
using System.Threading.Tasks;

public class ServiceController : Controller
{
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateServiceInputModel model)
    {
        if (ModelState.IsValid)
        {
            await _serviceService.CreateAsync(model);
            return RedirectToAction("Index");
        }

        return View(model);
    }

    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public async Task<IActionResult> Index()
    {
        var services = await _serviceService.GetAllAsync();
        return View(services);
    }

    public async Task<IActionResult> Details(int id)
    {
        var service = await _serviceService.GetByIdAsync(id);
        if (service == null) return NotFound();
        return View(service);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceInputModel model)
    {
        if (ModelState.IsValid)
        {
            await _serviceService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var service = await _serviceService.GetByIdAsync(id);
        if (service == null) return NotFound();

        var model = new UpdateServiceInputModel
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description,
            Price = service.Price,
            Category = service.Category,
            ImageUrl = service.ImageUrl,
            Availability = service.Availability
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateServiceInputModel model)
    {
        if (ModelState.IsValid)
        {
            await _serviceService.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var service = await _serviceService.GetByIdAsync(id);
        if (service == null) return NotFound();
        return View(service);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _serviceService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
