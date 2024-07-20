namespace RealState.Application.Interfaces.Repositories
{
    public interface IGenericRepositories<TEntity, TKey>
    where TEntity : Entity<TKey>
    {
        public Task<TEntity?> GetById(TKey key);
        public Task<ICollection<TEntity>> GetAll();
        public Task<TEntity> Update(TEntity entity);
        public Task<TEntity> Create(TEntity entity);
        public Task Delete(TKey key);
    }

    public interface IGenericRepositories<TEntity>
    : IGenericRepositories<TEntity, Guid>
    where TEntity : Entity<TKey>;
}