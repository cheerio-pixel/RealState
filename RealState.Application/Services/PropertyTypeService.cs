using AutoMapper;

using RealState.Application.DTOs.PropertyType;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters.PropertyType;
using RealState.Application.ViewModel.PropertyType;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    internal class PropertyTypeService
    : GenericService<PropertyTypeSaveViewModel, PropertyTypeViewModel, PropertyTypes>,
      IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public PropertyTypeService(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        : base(propertyTypeRepository, mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyTypeListItemViewModel>> SearchPropertyType(PropertyTypeQueryFilter filter)
        {
            List<PropertyTypeListItemDTO> propertyTypeListItemDTOs
                = await _propertyTypeRepository.ListPropertyTypes(filter);

            return _mapper.Map<List<PropertyTypeListItemViewModel>>(propertyTypeListItemDTOs);
        }
    }
}