using AutoMapper;

using RealState.Application.Extras.ResultObject;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Pictures;
using RealState.Application.ViewModel.Property;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    public class PropertyService(IPropertyRepository propertyRepository, IMapper mapper,
        IPropertyUpgradeService propertyUpgradeService,
        IPictureService pictureService) : GenericService<PropertSaveViewModel, PropertyViewModel, Properties>(propertyRepository, mapper), IPropertyService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPropertyUpgradeService _propertyUpgradeService = propertyUpgradeService;
        private readonly IPropertyRepository _propertyRepository = propertyRepository;
        private readonly IPictureService _pictureService = pictureService;

        public async override Task<Result<PropertSaveViewModel>> Add(PropertSaveViewModel vm)
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
            await base.Delete(id);
        }
    }
}
