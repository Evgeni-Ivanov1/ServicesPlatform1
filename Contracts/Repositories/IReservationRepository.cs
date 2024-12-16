using ServicesPlatform.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesPlatform.Contracts.Repositories
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task<Reservation> CreateAsync(Reservation reservation);
        Task DeleteAsync(int id);
    }
}
