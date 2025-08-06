using System.Security.Claims;
using WallOfKindness.Web.Areas.GeneralUser.ViewModel;

namespace WallOfKindness.Application.Users
{
    public interface IUserServices
    {
        Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterVM model);
        Task<(bool Succeeded, string ErrorMessage)> LoginAsync(LoginVM model);
        Task<ProfileVM?> GetProfileAsync(ClaimsPrincipal userPrincipal);
        Task LogoutAsync();
    }
}
