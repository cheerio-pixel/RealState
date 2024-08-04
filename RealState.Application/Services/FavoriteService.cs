

using AutoMapper;

using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Favorite;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    public class FavoriteService(IFavoriteRepository favoriteRepository, IMapper mapper) : GenericService<FavoriteSaveViewModel, FavoriteViewModel, Favorite>(favoriteRepository, mapper), IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository = favoriteRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<FavoriteViewModel>> GetAllFavoriteByUserId(Guid userId)
        {
            var favorites = await _favoriteRepository.GetAllFavoriteByUserId(userId);
            return _mapper.Map<List<FavoriteViewModel>>(favorites);
        }
    }
}
