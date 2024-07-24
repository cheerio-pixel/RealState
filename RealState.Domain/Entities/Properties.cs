using RealState.Domain.Common;

namespace RealState.Domain.Entities
{
    public class Properties : Entity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string Code { get; set; } = null!;
        public decimal Meters { get; set; }
        public List<string> Pictures { get; set; } = null!;
        public Guid PropertyTypeId { get; set; }
        public PropertyTypes PropertyTypes { get; set; } = null!;
        public Guid SalesTypeId { get; set; }
        public SalesTypes SalesTypes { get; set; } = null!;

        /*Navigation Properties*/
        public ICollection<PropertiesUpgrade> PropertiesUpgrades { get; set; } = null!;
        public ICollection<Favorite> Favorites { get; set; } = null!;
    }
}
