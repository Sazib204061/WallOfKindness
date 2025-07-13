using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MomotarJhuri.Domain.Entities;

namespace MomotarJhuri.Infractructure.Data.Configuration
{
    public class GiftDetailConfiguration : IEntityTypeConfiguration<GiftDetail>
    {
        public void Configure(EntityTypeBuilder<GiftDetail> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Description);
            builder.Property(d => d.Status);

            builder.HasData(
                new GiftDetail
                {
                    Id = 1,
                    Description = "Description of gift-1",
                    Status = Domain.Enums.GiftStatus.Available,
                    GiftId = 1,
                },
                new GiftDetail
                {
                    Id = 2,
                    Description = "Description of gift-2",
                    Status = Domain.Enums.GiftStatus.Available,
                    GiftId = 2,
                }
            );
        }
    }
}
