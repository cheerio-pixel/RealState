using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<List<Favorite>> GetAllFavoriteByUserId(Guid userId);
        Task DeleteByPropertyId(Guid propertyId);
    }
}