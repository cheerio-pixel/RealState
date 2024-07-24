using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Context.Configuration
{
    public class PropertiesUpgradeConfiguration : IEntityTypeConfiguration<PropertiesUpgrade>
    {
        public void Configure(EntityTypeBuilder<PropertiesUpgrade> builder)
        {

            builder.HasOne(e => e.Properties)
                .WithMany(e => e.PropertiesUpgrades)
                .HasForeignKey(e => e.PropertyId);

            builder.HasOne(e => e.Upgrades)
                .WithMany(e => e.PropertiesUpgrades)
                .HasForeignKey(e => e.UpgradeId);
        }
    }
}
