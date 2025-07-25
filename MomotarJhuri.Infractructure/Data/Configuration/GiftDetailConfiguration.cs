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
        }
    }
}
