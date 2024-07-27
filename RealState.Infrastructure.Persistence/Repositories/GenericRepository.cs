using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Common;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>
    : IGenericRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
    {
        private readonly MainContext _context;

        public GenericRepository(MainContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Checks that a property from entity exists
        /// </summary>
        protected async Task<bool> PropertyExistsWithValue<T>(Expression<Func<TEntity, T>> getter, T value, TKey keyToExclude)
        {
            ParameterExpression parameter = getter.Parameters[0];
            MemberExpression getId = Expression.MakeMemberAccess(parameter, typeof(Entity).GetMember("Id")[0]);
            Expression<Func<TEntity, bool>> lambdaExpression =
                Expression.Lambda<Func<TEntity, bool>>(
            Expression.AndAlso(
                Expression.Equal(getter.Body, Expression.Constant(value)),
                Expression.NotEqual(getId, Expression.Constant(keyToExclude))
            ), parameter
                    );
            return await _context.Set<TEntity>().AnyAsync(lambdaExpression);
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            TEntity? entry = await _context.Set<TEntity>().FindAsync(entity.Id);
            if (entry is null) return null;
            _context.Entry(entry).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entry;
        }

        public async Task Delete(TKey key)
        {
            var entry = await _context.Set<TEntity>().FindAsync(key);
            if (entry is null) return;
            _context.Set<TEntity>().Remove(entry);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetById(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }

    public class GenericRepository<TEntity>
    : GenericRepository<TEntity, Guid>,
      IGenericRepository<TEntity>
    where TEntity : Entity<Guid>
    {
        public GenericRepository(MainContext context) : base(context)
        {
        }
    }
}