using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.PropertyType;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Services
{
    public interface IPropertyTypeService
    : IGenericService<PropertyTypeSaveViewModel, PropertyTypeViewModel, PropertyTypes>
    {
        Task<List<PropertyTypeListItemViewModel>> SearchPropertyType(PropertyTypeQueryFilter filter);
    }
}