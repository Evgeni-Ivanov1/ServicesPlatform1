using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesPlatform.Contracts.Services
{
    public interface IUserService
    {
        Task<IEnumerable<IdentityUser>> GetUsersAsync();
        Task CreateRoleAsync(string roleName);
        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IEnumerable<IdentityRole>> GetRolesAsync();
        Task AssignRoleAsync(string userId, string roleName);

    }
}