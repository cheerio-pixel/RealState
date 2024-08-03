using RealState.Application.DTOs.PropertyType;
using RealState.Application.QueryFilters;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IPropertyTypeRepository
    : IGenericRepository<PropertyTypes>
    {
        Task<List<PropertyTypeListItemDTO>> ListPropertyTypesWithCount(PropertyTypeQueryFilter filter);
        Task<List<PropertyTypes>> ListPropertyTypes(PropertyTypeQueryFilter filter);
        Task<bool> DoesPropertyTypeNameExists(string name, Guid? idToExclude);
    }
}