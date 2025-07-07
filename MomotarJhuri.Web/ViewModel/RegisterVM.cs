using MomotarJhuri.Domain.Enums;

namespace MomotarJhuri.Web.ViewModel
{
    public class RegisterVM
    {
        public string UserName { get; set; } = string.Empty;    
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
