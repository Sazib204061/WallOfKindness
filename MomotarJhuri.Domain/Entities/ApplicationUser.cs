using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WallOfKindness.Domain.Enums;

namespace WallOfKindness.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, Display(Name ="Full Name")]
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? PhotoLink { get; set; }
        public string? Nationality { get; set; }
        public Gender Gender { get; set; }
        public ICollection<Gift> Gifts { get; set; }
    }
}
