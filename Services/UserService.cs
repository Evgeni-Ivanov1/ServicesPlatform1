﻿using Microsoft.AspNetCore.Identity;
using ServicesPlatform.Contracts.Services;
using ServicesPlatform.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesPlatform.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return _userManager.Users.ToList();
        }

        public async Task CreateRoleAsync(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !string.IsNullOrWhiteSpace(roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }

        Task<IEnumerable<IdentityUser>> IUserService.GetUsersAsync()
        {
            throw new System.NotImplementedException();
        }

        Task<IdentityUser> IUserService.GetUserByIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}