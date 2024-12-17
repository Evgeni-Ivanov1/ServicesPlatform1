using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _serviceRepository.GetAllAsync();
    }
    public async Task<Service> GetByIdWithReviewsAsync(int id)
    {
        return await _serviceRepository.GetByIdWithReviewsAsync(id);
    }

    public async Task<Service> GetByIdAsync(int id)
    {
        return await _serviceRepository.GetByIdAsync(id);
    }

   public async Task<Service> CreateAsync(CreateServiceInputModel model)
{
    var service = new Service
    {
        Name = model.Name,
        Description = model.Description,
        Price = model.Price,
        CategoryId = model.CategoryId,
        ImageUrl = model.ImageUrl,
        Availability = model.Availability,
        OwnerId = model.CreatorId,
        CreatedOn = DateTime.Now
    };

    return await _serviceRepository.CreateAsync(service);
}

public async Task<Service> UpdateAsync(UpdateServiceInputModel model)
{
    var service = await _serviceRepository.GetByIdAsync(model.Id);
    if (service == null)
    {
        throw new KeyNotFoundException("Service not found");
    }

    service.Name = model.Name;
    service.Description = model.Description;
    service.Price = model.Price;
    service.CategoryId = model.CategoryId;
    service.ImageUrl = model.ImageUrl;
    service.Availability = model.Availability;

    return await _serviceRepository.UpdateAsync(service);
}

    public async Task DeleteAsync(int id)
    {
        await _serviceRepository.DeleteAsync(id);
    }

    public Task<Service> CreateAsync(Service service)
    {
        throw new NotImplementedException();
    }

    public Task<Service> UpdateAsync(Service service)
    {
        throw new NotImplementedException();
    }
}
