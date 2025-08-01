using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WallOfKindness.Domain.Entities;
using WallOfKindness.Web.Areas.GeneralUser.ViewModel;

namespace WallOfKindness.Web.Areas.GeneralUser.Controllers
{

    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ApplicationUser> _logger;
        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<ApplicationUser> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                //convert FullName to user name. Because without username Data cannot insert into Identity database.
                var temp = model.FullName;
                temp = temp.Trim().ToLower();
                temp = temp.Replace(" ", "");
                var user = new ApplicationUser
                {
                    UserName = temp,
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    PhotoLink = model.PhotoLink,
                    Nationality = model.Nationality,
                    Gender = model.Gender
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName,  // Use UserName here
                    model.Password,
                    isPersistent: false,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User {UserName} logged in.", user.UserName);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }


        [HttpGet]
        [Authorize] // Ensure only logged-in users can access
        public async Task<IActionResult> Profile()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            //user = null;

            if (user == null)
            {
                return Challenge(); // Will redirect to login if user not found
            }

            // Map to a view model
            var model = new ProfileVM
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                PhotoLink = user.PhotoLink,
                Nationality = user.Nationality,
                Gender = user.Gender
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        
    }
}
