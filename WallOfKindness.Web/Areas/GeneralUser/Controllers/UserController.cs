using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WallOfKindness.Application.Users;
using WallOfKindness.Domain.Entities;
using WallOfKindness.Web.Areas.GeneralUser.ViewModel;

namespace WallOfKindness.Web.Areas.GeneralUser.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var (succeeded, errors) = await _userServices.RegisterAsync(model);

            if (succeeded)
                return RedirectToAction("Login", "User");

            foreach (var error in errors)
                ModelState.AddModelError(string.Empty, error);

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var (succeeded, errorMessage) = await _userServices.LoginAsync(model);

            if (succeeded)
                return RedirectToAction("Index", "Gift");

            ModelState.AddModelError(string.Empty, errorMessage);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var profile = await _userServices.GetProfileAsync(User);
            if (profile == null) return Challenge();

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userServices.LogoutAsync();
            return RedirectToAction("Index", "Gift");
        }
    }

}
