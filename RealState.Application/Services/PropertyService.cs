using AutoMapper;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Property;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    public class PropertyService(IPropertyRepository propertyRepository, IMapper mapper) : GenericService<PropertSaveViewModel, PropertyViewModel, Properties>(propertyRepository, mapper), IPropertyService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPropertyRepository _propertyRepository = propertyRepository;

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
       
    }
}
