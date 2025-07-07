using Microsoft.EntityFrameworkCore;
using MomotarJhuri.Domain.Entities;

namespace MomotarJhuri.Infractructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //public DbSet<Gift> Gifts { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Gift>().HasData(
            //    new Gift
            //    {
            //        Id = 1,
            //        Location = "Dhaka",
            //        UserId = 111 // Assuming a user with ID 1 exists
            //    },
            //    new Gift
            //    {
            //        Id = 2,
            //        Location = "Cumilla",
            //        UserId = 112 // Assuming a user with ID 1 exists
            //    },
            //    new Gift
            //    {
            //        Id = 3,
            //        Location = "Syleth",
            //        UserId = 114 // Assuming a user with ID 1 exists
            //    }
            //);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "sr",
                    Email = "sr@gmail.com",
                    Password = "password",
                    PhoneNumber = "01700000000",
                    Role = Domain.Enums.UserRole.User,
                    Address = "Dhaka",
                    PhotoLink = "https://example.com/photo.jpg",
                    Nationality = "Bangladeshi",
                    Gender = Domain.Enums.Gender.Male,
                },
                new User
                {
                    Id = 2,
                    Name = "tf",
                    Email = "tf@gmail.com",
                    Password = "password",
                    PhoneNumber = "01700000000",
                    Role = Domain.Enums.UserRole.User,
                    Address = "Dhaka2",
                    PhotoLink = "https://example.com/photo.jpg",
                    Nationality = "Bangladeshi",
                    Gender = Domain.Enums.Gender.Male,
                }

            );
        }
    }
}
