using Microsoft.EntityFrameworkCore;
using ReservationPlatform.Data;
using ServicesPlatform.Contracts.Repositories;
using ServicesPlatform.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesPlatform.Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await GetByIdAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
