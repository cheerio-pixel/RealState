using RealState.Domain.Common;

namespace RealState.Domain.Entities
{
    public class PropertiesUpgrades : Entity
    {
        public Guid PropertyId { get; set; }
        public Guid UpgradeId { get; set; }
        public Properties Property { get; set; } = null!;
        public Upgrades Upgrade { get; set; } = null!;
    }
}
