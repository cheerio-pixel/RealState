using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Context.Configuration
{
    internal class PropertiesConfiguration : IEntityTypeConfiguration<Properties>
    {
        public void Configure(EntityTypeBuilder<Properties> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.Rooms)
                .IsRequired();

            builder.Property(p => p.Bathrooms)
                .IsRequired();

            builder.HasMany(x => x.PropertiesUpgrades)
                   .WithOne(x => x.Property)
                   .HasForeignKey(x => x.PropertyId);

            builder.HasOne(p => p.PropertyTypes)
                .WithMany(p => p.Properties)
                .HasForeignKey(p => p.PropertyTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.SalesTypes)
                .WithMany(p => p.Properties)
                .HasForeignKey(p => p.SalesTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Pictures)
                .WithOne(p => p.Property)
                .HasForeignKey(p => p.PropertyId);

        }
    }

}
