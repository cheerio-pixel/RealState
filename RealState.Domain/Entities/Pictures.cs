using RealState.Domain.Common;

namespace RealState.Domain.Entities
{
    public class Pictures : Entity
    {
        public string Picture { get; set; } = null!;
        public Guid PropertyId { get; set; }
        public Properties Property { get; set; } = null!;
    }
}
