namespace RealState.Domain.Common
{
    public class Entity<TKey>
    {
        public TKey? Id { get; set; }
    }

    public class Entity
    : Entity<Guid>;
}