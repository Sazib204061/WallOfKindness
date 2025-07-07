using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MomotarJhuri.Domain.Entities;
using MomotarJhuri.Web.ViewModel;

namespace MomotarJhuri.Web.Controllers
{

    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    PhotoLink = model.PhotoLink,
                    Nationality = model.Nationality,
                    Gender = model.Gender
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) { 
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
    }
}
