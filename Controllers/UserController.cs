using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Contracts.Services;
using System.Threading.Tasks;

namespace ReservationPlatform.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAsync();
            return View(users);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            await _userService.CreateRoleAsync(roleName);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignRole(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userService.GetRolesAsync();
            ViewBag.Roles = roles;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            await _userService.AssignRoleAsync(userId, roleName);
            return RedirectToAction(nameof(Index));
        }
    }
}
