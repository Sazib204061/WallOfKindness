using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using WallOfKindness.Domain.Entities;
using WallOfKindness.Web.Areas.GeneralUser.ViewModel;
using WallOfKindness.Application.Users;

namespace WallOfKindness.Application.Users
{
    public class UserServices : IUserServices
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserServices> _logger;

        public UserServices(SignInManager<ApplicationUser> signInManager,
                            UserManager<ApplicationUser> userManager,
                            ILogger<UserServices> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterVM model)
        {
            var username = model.FullName.Trim().ToLower().Replace(" ", "");

            var user = new ApplicationUser
            {
                UserName = username,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PhotoLink = model.PhotoLink,
                Nationality = model.Nationality,
                Gender = model.Gender
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return (result.Succeeded, result.Errors.Select(e => e.Description));
        }

        public async Task<(bool Succeeded, string ErrorMessage)> LoginAsync(LoginVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (false, "Invalid login attempt.");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

            if (!result.Succeeded)
                return (false, "Invalid login attempt.");

            _logger.LogInformation("User {UserName} logged in.", user.UserName);
            return (true, string.Empty);
        }

        public async Task<ProfileVM?> GetProfileAsync(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null) return null;

            return new ProfileVM
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                PhotoLink = user.PhotoLink,
                Nationality = user.Nationality,
                Gender = user.Gender
            };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }
    }
}
