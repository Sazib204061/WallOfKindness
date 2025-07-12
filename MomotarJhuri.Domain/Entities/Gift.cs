using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MomotarJhuri.Domain.Entities
{
    public class Gift
    {
        public int Id { get; set; }
        public string? Location { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        //[ValidateNever]
        //public User? User { get; set; } // Navigation property to User entity
    }
}
