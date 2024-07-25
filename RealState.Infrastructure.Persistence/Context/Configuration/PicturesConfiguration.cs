using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Context.Configuration
{
    public class PicturesConfiguration : IEntityTypeConfiguration<Pictures>
    {
        public void Configure(EntityTypeBuilder<Pictures> builder)
        {

            builder.Property(p => p.Picture)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(x => x.Property)
                .WithMany(x => x.Pictures)
                .HasForeignKey(x => x.PropertyId);
        }
    }
}
