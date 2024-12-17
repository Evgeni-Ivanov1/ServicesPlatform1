using ServicesPlatform.Models.InputModels.Service;
using ServicesPlatform.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesPlatform.Contracts.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(int id);
   
        Task<Service> GetByIdWithReviewsAsync(int id);
 
        Task<Service> CreateAsync(CreateServiceInputModel model); 
        Task<Service> UpdateAsync(UpdateServiceInputModel model);
        Task<IEnumerable<Service>> GetServicesByOwnerIdAsync(string ownerId);

        Task DeleteAsync(int id);
      
    }
}
