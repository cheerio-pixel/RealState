using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Context.Configuration
{
    internal class SalesTypesConfiguration : IEntityTypeConfiguration<SalesTypes>
    {
        public void Configure(EntityTypeBuilder<SalesTypes> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(p => p.Properties)
                .WithOne(p => p.SalesTypes)
                .HasForeignKey(p => p.SalesTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
