using Microsoft.EntityFrameworkCore;

using RealState.Application.DTOs.PropertyType;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.QueryFilters;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    internal class PropertyTypeRepository
    : GenericRepository<PropertyTypes>, IPropertyTypeRepository
    {
        private readonly MainContext _context;

        public PropertyTypeRepository(MainContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<PropertyTypes> Filter(PropertyTypeQueryFilter filter)
        {
            IQueryable<PropertyTypes> propertyTypeses = _context.PropertyTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                propertyTypeses = propertyTypeses.Where(pt => pt.Name.Contains(filter.Name));
            }
            return propertyTypeses;
        }

        public async Task<bool> DoesPropertyTypeNameExists(string name, Guid? idToExclude)
        {
            // return await _context.PropertyTypes.AnyAsync(t => t.Name == name && t.Id != idToExclude);
            return await PropertyExistsWithValue(e => e.Name, name, idToExclude.GetValueOrDefault());
        }

        public async Task<List<PropertyTypeListItemDTO>> ListPropertyTypesWithCount(PropertyTypeQueryFilter filter)
        {
            IQueryable<PropertyTypes> propertyTypeses = Filter(filter);
            return await propertyTypeses
                         .AsNoTracking()
                         .Select(pt => new PropertyTypeListItemDTO()
                         {
                             Id = pt.Id,
                             Name = pt.Name,
                             Description = pt.Description,
                             NumberOfProperties = pt.Properties.Count
                         })
                         .ToListAsync();
        }

        public async Task<List<PropertyTypes>> ListPropertyTypes(PropertyTypeQueryFilter filter)
        {
            return await Filter(filter).ToListAsync();
        }
    }
}