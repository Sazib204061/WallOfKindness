using System.ComponentModel.DataAnnotations;
using WallOfKindness.Domain.Enums;

namespace WallOfKindness.Web.Areas.GeneralUser.ViewModel
{
    public class RegisterVM
    {
        public string FullName { get; set; } = string.Empty;    
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        //public UserRole Role { get; set; }
        public string? Address { get; set; }
        public string? PhotoLink { get; set; }
        public string? Nationality { get; set; }
        public Gender Gender { get; set; }


    }
}
