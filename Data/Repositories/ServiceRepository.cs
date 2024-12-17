using ServicesPlatform.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReservationPlatform.Data;
using ServicesPlatform.Contracts.Repositories;
using System.Linq;

public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _context.Services.Include(s => s.Category).ToListAsync();
    }

    public async Task<Service> GetByIdAsync(int id)
    {
        return await _context.Services.Include(s => s.Category).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Service> CreateAsync(Service service)
    {
        var result = await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Service> UpdateAsync(Service service)
    {
        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task DeleteAsync(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service != null)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<Service> GetByIdWithReviewsAsync(int id)
    {
        return await _context.Services
            .Include(s => s.Reviews)
            .ThenInclude(r => r.User) 
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<IEnumerable<Service>> GetServicesByOwnerIdAsync(string ownerId)
    {
        return await _context.Services
            .Include(s => s.Category) 
            .Where(s => s.OwnerId == ownerId) 
            .ToListAsync();
    }



}
