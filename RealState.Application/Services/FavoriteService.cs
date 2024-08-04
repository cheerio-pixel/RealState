

using AutoMapper;

using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Favorite;
using RealState.Application.ViewModel.Property;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    public class FavoriteService(IFavoriteRepository favoriteRepository, 
                                 IMapper mapper,
                                  IPropertyRepository propertyRepository) : GenericService<FavoriteSaveViewModel, FavoriteViewModel, Favorite>(favoriteRepository, mapper), IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository = favoriteRepository;
        private readonly IPropertyRepository _propertyService = propertyRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<FavoriteViewModel>> GetAllFavoriteByUserId(Guid userId)
        {
            var favorites = await _favoriteRepository.GetAllFavoriteByUserId(userId);
            return _mapper.Map<List<FavoriteViewModel>>(favorites);
        }

        public async Task<List<FavoriteViewModel>> FavoriteViewModels(Guid userId)
        {
            var favorites = await _favoriteRepository.GetAllFavoriteByUserId(userId);
            var favoriteViewModels = _mapper.Map<List<FavoriteViewModel>>(favorites);
            foreach (var favorite in favoriteViewModels)
            {
                favorite.Property = _mapper.Map<PropertyViewModel>(await _propertyService.GetByIdWithInclude(favorite.PropertyId));
            }
            return favoriteViewModels;
        }

    }
}
