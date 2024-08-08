
using Microsoft.EntityFrameworkCore;

using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.QueryFilters;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository(MainContext context)
    : GenericRepository<Properties>(context), IPropertyRepository
    {
        private readonly DbSet<Properties> _properties = context.Set<Properties>();

        public async Task<bool> IsCodeUnique(string code)
        {
            return !await _properties.AnyAsync(x => x.Code == code);
        }

        public async Task<Properties?> GetByIdWithPictures(Guid id)
        {
            return await _properties.Include(x => x.Pictures).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Properties>> GetPropertyByAgentId(Guid agentId)
        {
            return await _properties.Where(x => x.AgentId == agentId).Include(x => x.Pictures).ToListAsync();
        }

        public async Task<List<Properties>> ListProperties(PropertyQueryFilter filter)
        {
            IQueryable<Properties> properties = _properties.AsQueryable();
            if(filter.PropertyTypeId != null)
            {
                properties = properties.Where(x => x.PropertyTypeId == filter.PropertyTypeId);
            }
            if(filter.AgentId != null)
            {
                properties = properties.Where(x => x.AgentId == filter.AgentId);
            }
            if (filter.Bathrooms != 0)
            {
                properties = properties.Where(x => x.Bathrooms == filter.Bathrooms);
            }
            if (filter.Rooms != 0)
            {
                properties = properties.Where(x => x.Rooms == filter.Rooms);
            }
            if (filter.MinPrice != 0)
            {
                properties = properties.Where(x => x.Price >= filter.MinPrice);
            }
            if (filter.MaxPrice != 0)
            {
                properties = properties.Where(x => x.Price <= filter.MaxPrice);
            }

            return await properties.Include(x => x.Pictures).Include(x => x.PropertyTypes).Include(x => x.SalesTypes).ToListAsync();
        }

        public async Task<Properties?> GetByIdWithInclude(Guid id)
        {
            return await _properties
                .Include(x => x.Pictures)
                .Include(x => x.PropertyTypes)
                .Include(x => x.SalesTypes)
                .Include(x => x.PropertiesUpgrades)
                .ThenInclude(x => x.Upgrade)
                .FirstOrDefaultAsync(x => x.Id == id)!;
        }

        public async Task<List<Properties>> GetPropertyByAgentIdWithInclude(Guid agentId)
        {
            return await _properties
                .Where(x => x.AgentId == agentId)
                .Include(x => x.Pictures)
                .Include(x => x.PropertyTypes)
                .Include(x => x.SalesTypes)
                .Include(x => x.PropertiesUpgrades)
                .ThenInclude(x => x.Upgrade)
                .ToListAsync();
        }

        public Task<int> GetNumberOfPropertiesOfAgent(Guid agentId)
        {
            return _properties.Where(p => p.AgentId == agentId)
                            .Count()
                            .AsTask();
        }

        public async Task<IEnumerable<Guid>> GetPropertyIdsByAgentId(Guid agentId)
        {
            return await (
                from p in _properties
                where p.AgentId == agentId
                select p.Id
            ).ToListAsync();
        }

        public async Task<Properties?> GetByCode(string code)
        {
            return await _properties.Where(p => p.Code == code).FirstOrDefaultAsync();
        }

        public Task<int> GetPropertyCount()
        {
            return _properties.Count().AsTask();
        }
    }
}
