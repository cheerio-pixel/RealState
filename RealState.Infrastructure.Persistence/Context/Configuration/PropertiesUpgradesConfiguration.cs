using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Context.Configuration
{
    public class PropertiesUpgradesConfiguration : IEntityTypeConfiguration<PropertiesUpgrades>
    {
        public void Configure(EntityTypeBuilder<PropertiesUpgrades> builder)
        {

            builder.HasOne(x => x.Property)
                .WithMany(x => x.PropertiesUpgrades)
                .HasForeignKey(x => x.PropertyId);

            builder.HasOne(x => x.Upgrade)
                .WithMany(x => x.PropertiesUpgrades)
                .HasForeignKey(x => x.UpgradeId);
        }
    }
}
