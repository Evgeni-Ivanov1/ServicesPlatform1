using ServicesPlatform.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesPlatform.Contracts.Repositories;
public interface IServiceRepository

{
    
        Task<Service> GetByIdWithReviewsAsync(int id);
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service> GetByIdAsync(int id);
    Task<Service> CreateAsync(Service service);
    Task<Service> UpdateAsync(Service service);
    Task<IEnumerable<Service>> GetServicesByOwnerIdAsync(string ownerId);
    Task DeleteAsync(int id);
}
