using MomotarJhuri.Domain.Enums;

namespace MomotarJhuri.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public string? Address { get; set; }
        public string? PhotoLink { get; set; }
        public string? Nationality { get; set; }
        public Gender Gender { get; set; }

    }
}
