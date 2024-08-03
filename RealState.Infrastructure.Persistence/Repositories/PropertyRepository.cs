using Microsoft.EntityFrameworkCore;

using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository(MainContext context) : GenericRepository<Properties>(context), IPropertyRepository
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

        public async Task<List<Properties>> GetAllWithInclude()
        {
            return await _properties.Include(x => x.Pictures).Include(x => x.PropertyTypes).Include(x => x.SalesTypes).ToListAsync();
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
    }
}
