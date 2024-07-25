using RealState.Domain.Common;

namespace RealState.Domain.Entities
{
    public class PropertiesUpgrades : Entity
    {
        public int PropertyId { get; set; }
        public int UpgradeId { get; set; }
        public Properties Property { get; set; } = null!;
        public Upgrades Upgrade { get; set; } = null!;
    }
}
