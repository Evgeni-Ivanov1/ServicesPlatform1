using System.Collections.Generic;
using System.Threading.Tasks;
using ServicesPlatform.Data.Models;

public interface IServiceService
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service> GetByIdAsync(int id);
    Task<Service> CreateAsync(CreateServiceInputModel model);
    Task<Service> UpdateAsync(UpdateServiceInputModel model);
    Task DeleteAsync(int id);
}
