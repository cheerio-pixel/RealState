using RealState.Domain.Common;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
    {
        public Task<TEntity?> GetById(TKey key);
        public Task<List<TEntity>> GetAll();
        public Task<TEntity?> Update(TEntity entity);
        public Task<TEntity> Create(TEntity entity);
        public Task Delete(TKey key);
    }

    public interface IGenericRepository<TEntity>
    : IGenericRepository<TEntity, Guid>
    where TEntity : Entity<Guid>;
}