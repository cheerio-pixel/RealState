using AutoMapper;

using RealState.Application.Extras.ResultObject;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.Property;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    public class PropertyService(IPropertyRepository propertyRepository, IMapper mapper,
        IPropertyUpgradeService propertyUpgradeService,
        IPictureService pictureService,
        IFavoriteRepository favoriteRepository,
        IUserRepository userRepository) : GenericService<PropertSaveViewModel, PropertyViewModel, Properties>(propertyRepository, mapper), IPropertyService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPropertyUpgradeService _propertyUpgradeService = propertyUpgradeService;
        private readonly IPropertyRepository _propertyRepository = propertyRepository;
        private readonly IPictureService _pictureService = pictureService;
        private readonly IFavoriteRepository _favoriteRepository = favoriteRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public override async Task<Result<PropertSaveViewModel>> Add(PropertSaveViewModel vm)
        {
            var property = _mapper.Map<Properties>(vm);

            var code = UniqueCodeGenerator.UniqueCode();

            while (!await _propertyRepository.IsCodeUnique(code))
            {
                code = UniqueCodeGenerator.UniqueCode();
            }
            property.Code = code;

            var propertyCreated = await _propertyRepository.Create(property);
            return _mapper.Map<PropertSaveViewModel>(propertyCreated);
        }

        public async Task<Result<PropertyViewModel>> GetByIdWithPictures(Guid id)
        {
            var property = await _propertyRepository.GetByIdWithPictures(id);
            return _mapper.Map<PropertyViewModel>(property);
        }

        public override async Task<PropertSaveViewModel?> GetByIdSaveViewModel(Guid id)
        {
            var property = await base.GetByIdSaveViewModel(id);
            if (property is null)
            {
                return null;
            }
            var upgrades = await _propertyUpgradeService.GetAllByPropertyId(id);
            property!.UpgradeId = upgrades.Select(x => x.UpgradeId).ToList();
            var pictures = await _pictureService.GetAllByPropertyId(id);
            property.PicturesUrl = pictures.Value.Select(x => x.Picture).ToList();
            return property;
        }

        public async Task<Result<List<PropertyViewModel>>> GetPropertyByAgentId(Guid agentId)
        {
            var properties = await _propertyRepository.GetPropertyByAgentId(agentId);
            return _mapper.Map<List<PropertyViewModel>>(properties);
        }

        public override async Task Delete(Guid id)
        {
            var property = await _propertyRepository.GetByIdWithPictures(id);
            if (property is null)
            {
                return;
            }

            await _pictureService.DeleteByPropertyId(id);
            await _propertyUpgradeService.DeleteByPropertyId(id);
            await _favoriteRepository.DeleteByPropertyId(id);
            await base.Delete(id);
        }

        public async Task DeletePropertiesOfAgent(Guid agentId)
        {
            IEnumerable<Guid> ids = await _propertyRepository.GetPropertyIdsByAgentId(agentId);
            await Task.WhenAll(ids.Select(Delete));
        }

        public async Task<Result<List<PropertyViewModel>>> ListPropertiesQueryable(PropertyQueryFilter filter)
        {
            List<Properties> properties = await _propertyRepository.ListProperties(filter);
            return _mapper.Map<List<PropertyViewModel>>(
                properties
            );
        }

        public async Task<Result<PropertyDetailsViewModel>> GetPropertyDetailsById(Guid id)
        {
            var property = await _propertyRepository.GetByIdWithInclude(id);
            var propertyMapper = _mapper.Map<PropertyDetailsViewModel>(property);
            var userResult = await _userRepository.Get(property!.AgentId.ToString());
            propertyMapper.ApplicationUser = userResult!;
            return propertyMapper;
        }

        public async Task<Result<List<PropertyViewModel>>> GetPropertyByAgentIdWithInclude(Guid agentId)
        {
            var properties = await _propertyRepository.GetPropertyByAgentIdWithInclude(agentId);
            return _mapper.Map<List<PropertyViewModel>>(properties);
        }

        public async Task<int> GetPropertyCount()
        {
            return await _propertyRepository.GetPropertyCount();
        }
    }
}
