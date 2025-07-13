using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MomotarJhuri.Domain.Entities;
using MomotarJhuri.Domain.Enums;

namespace MomotarJhuri.Infractructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Gift> Gifts { get; set; }
        public DbSet<GiftDetail> GiftDetail { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Gift configuratuion
            modelBuilder.Entity<Gift>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Title).IsRequired().HasMaxLength(100);
                entity.Property(g => g.Location).IsRequired();

                //one to one relationship with GiftDetail
                entity.HasOne(g => g.Detail)
                      .WithOne(d => d.Gift)
                      .HasForeignKey<GiftDetail>(d => d.GiftId)
                      .OnDelete(DeleteBehavior.Cascade);
                //one to many relationship with Image
                entity.HasMany(g => g.Images)
                      .WithOne(i => i.Gift)
                      .HasForeignKey(i => i.GiftId)
                      .OnDelete(DeleteBehavior.Cascade);  //যখন একটা গিপ্ট ডিলেট করবে তখন ইমেইজ টেবিল থেকেও ইমেইজ ডিলিট হবে।
            });

            modelBuilder.Entity<GiftDetail>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Description);
                entity.Property(d => d.Status);

            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.ImageUrl);
            });
        }

    }
}
