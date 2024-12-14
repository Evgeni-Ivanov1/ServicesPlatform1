using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models;
using ServicesPlatform.Models.InputModels;
using ServicesPlatform.Models.ViewModels;
using System.Threading.Tasks;

namespace ReservationPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid email");
                return View(model);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (isPasswordValid)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Invalid password");
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Manage(string section)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new ManageAccountViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            ViewData["Section"] = section ?? "Profile";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(ManageAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data. Please try again.";
                return RedirectToAction("Manage", new { tab = "Profile" });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Manage", new { tab = "Profile" });
            }

            user.FullName = model.Name;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profile updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update profile. Please try again.";
            }

            return RedirectToAction("Manage", new { tab = "Profile" });
        }


        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ManageAccountViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            user.Email = model.NewEmail;
            user.UserName = model.NewEmail;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Your email has been updated successfully!";
                return RedirectToAction("Manage", new { section = "Email" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            ViewData["Section"] = "Email";
            return View("Manage", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ManageAccountViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Your password has been updated successfully!";
                return RedirectToAction("Manage", new { section = "Password" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            ViewData["Section"] = "Password";
            return View("Manage", model);
        }
    
    
[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    } }






   


