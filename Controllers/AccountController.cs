using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServicesPlatform.Data.Models;
using ServicesPlatform.Models;
using ServicesPlatform.Models.InputModels;
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
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (isPasswordValid)
            {
                await _signInManager.SignInAsync(user, false);
                return View();
            }
            else
            {
                ModelState.AddModelError("Password", "Invalid password");
            }

            return View();
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


    }
        }
  
        
        
 