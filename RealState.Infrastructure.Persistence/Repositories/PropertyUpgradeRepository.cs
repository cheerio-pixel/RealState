using Microsoft.EntityFrameworkCore;

using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropertyUpgradeRepository(MainContext context) : GenericRepository<PropertiesUpgrades>(context), IPropertyUpgradeRepository
    {
        private readonly DbSet<PropertiesUpgrades> _propertiesUpgrades = context.Set<PropertiesUpgrades>();
        private readonly MainContext _context = context;

        public async Task<List<PropertiesUpgrades>> GetAllByPropertyId(Guid propertyId)
        {
            return await _propertiesUpgrades
                .Include(x => x.Upgrade)
                .Where(x => x.PropertyId == propertyId)
                .ToListAsync();
        }
    }
}
