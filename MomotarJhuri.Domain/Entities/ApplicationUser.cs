using Microsoft.AspNetCore.Identity;
using MomotarJhuri.Domain.Enums;

namespace MomotarJhuri.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public string? PhotoLink { get; set; }
        public string? Nationality { get; set; }
        public Gender Gender { get; set; }
    }
}
