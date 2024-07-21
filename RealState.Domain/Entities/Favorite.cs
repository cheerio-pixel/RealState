using RealState.Domain.Common;

namespace RealState.Domain.Entities
{
    public class Favorite : Entity
    {
        public Guid UserId { get; set; }
        public Guid PropertyId { get; set; }
        public Properties Property { get; set; } = null!;
    }
}
