using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Context.Configuration
{
    public class PropertyTypesConfiguration : IEntityTypeConfiguration<PropertyTypes>
    {
        public void Configure(EntityTypeBuilder<PropertyTypes> builder)
        {

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(p => p.Properties)
                .WithOne(p => p.PropertyTypes)
                .HasForeignKey(p => p.PropertyTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
