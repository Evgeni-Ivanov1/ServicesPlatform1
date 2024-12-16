using ServicesPlatform.Models.InputModels.Reservation;
using ServicesPlatform.Models.OutputModels.Reservation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesPlatform.Contracts.Services
{
    public interface IReservationService
    {
        Task<List<ReservationViewModel>> GetAllAsync();
        Task<ReservationViewModel> CreateAsync(CreateReservationInputModel model);
        Task DeleteAsync(int id);
    }
}
