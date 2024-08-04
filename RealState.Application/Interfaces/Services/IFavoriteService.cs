using RealState.Application.ViewModel.Favorite;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Services
{
    public interface IFavoriteService : IGenericService<FavoriteSaveViewModel, FavoriteViewModel, Favorite>
    {
        Task<List<FavoriteViewModel>> GetAllFavoriteByUserId(Guid userId);
        Task<List<FavoriteViewModel>> FavoriteViewModels(Guid userId);
    }
}