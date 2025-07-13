using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MomotarJhuri.Domain.Entities;

namespace MomotarJhuri.Infractructure.Data.Configuration
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ImageUrl);

            builder.HasData(
                new Image
                {
                    Id = 1,
                    GiftId = 1,
                    ImageUrl = "images/gifts/birthday-package.jpg"
                },
                new Image
                {
                    Id = 2,
                    GiftId = 1,
                    ImageUrl = "images/gifts/birthday-package-alt.jpg"
                },
                new Image
                {
                    Id = 3,
                    GiftId = 2,
                    ImageUrl = "images/gifts/anniversary-special.jpg"
                }
            );

        }
    }
}
