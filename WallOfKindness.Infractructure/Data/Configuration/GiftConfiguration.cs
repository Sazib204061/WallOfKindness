using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WallOfKindness.Domain.Entities;

namespace WallOfKindness.Infractructure.Data.Configuration
{
    public class GiftConfiguration : IEntityTypeConfiguration<Gift>
    {
        public void Configure(EntityTypeBuilder<Gift> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Title).IsRequired().HasMaxLength(100);
            builder.Property(g => g.Location).IsRequired();

            //one to one relationship with GiftDetail
            builder.HasOne(g => g.Detail)
                  .WithOne(d => d.Gift)
                  .HasForeignKey<GiftDetail>(d => d.GiftId)
                  .OnDelete(DeleteBehavior.Cascade);

            //one to many relationship with Image
            builder.HasMany(g => g.Images)
                  .WithOne(i => i.Gift)
                  .HasForeignKey(i => i.GiftId)
                  .OnDelete(DeleteBehavior.Cascade);  //যখন একটা গিপ্ট ডিলেট করবে তখন ইমেইজ টেবিল থেকেও ইমেইজ ডিলিট হবে।

            builder.HasOne(g => g.User)
                  .WithMany(u => u.Gifts) // If you don't have a navigation property back to Gift in ApplicationUser
                  .HasForeignKey(g => g.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
