using Microsoft.EntityFrameworkCore;

using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class FavoriteRepository(MainContext context) : GenericRepository<Favorite>(context), IFavoriteRepository
    {
        private readonly DbSet<Favorite> _favorites = context.Set<Favorite>();
        private readonly MainContext _mainContext = context;

        public async Task<List<Favorite>> GetAllFavoriteByUserId(Guid userId)
        {
            return await _favorites.Where(x => x.UserId == userId).Include(x => x.Property).ToListAsync();
        }
        public async Task DeleteByPropertyId(Guid propertyId)
        {
            var favorite = await _favorites.Where(x => x.PropertyId == propertyId).ToListAsync();
            _favorites.RemoveRange(favorite);
            await _mainContext.SaveChangesAsync();

        }
    }
}
