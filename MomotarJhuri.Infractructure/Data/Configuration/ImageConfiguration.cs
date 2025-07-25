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

        }
    }
}
