using RealState.Domain.Common;

namespace RealState.Domain.Entities
{
    public class PropertiesUpgrade : Entity
    {
        public Guid PropertyId { get; set; }
        public Properties Properties { get; set; } = null!;
        public Guid UpgradeId { get; set; }
        public Upgrades Upgrades { get; set; } = null!;
    }
}
