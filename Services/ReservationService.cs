using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models.InputModels.Reservation;
using ServicesPlatform.Models.OutputModels.Reservation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesPlatform.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;

        public ReservationService(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ReservationViewModel>> GetAllAsync()
        {
            var reservations = await _repository.GetAllAsync();
            return reservations.Select(r => new ReservationViewModel
            {
                Id = r.Id,
                ServiceName = r.ServiceName,
                ReservationDate = r.ReservationDate,
                Price = r.Price,
                Username = r.Username
            }).ToList();
        }

        public async Task<ReservationViewModel> CreateAsync(CreateReservationInputModel model)
        {
            var reservation = new Reservation
            {
                ServiceId = model.ServiceId,
                ServiceName = model.ServiceName,
                ReservationDate = model.ReservationDate,
                Price = model.Price
            };

            var createdReservation = await _repository.CreateAsync(reservation);

            return new ReservationViewModel
            {
                Id = createdReservation.Id,
                ServiceName = createdReservation.ServiceName,
                ReservationDate = createdReservation.ReservationDate,
                Price = createdReservation.Price
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
