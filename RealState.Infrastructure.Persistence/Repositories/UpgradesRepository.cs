using Microsoft.EntityFrameworkCore;

using RealState.Application.Interfaces.Repositories;
using RealState.Application.QueryFilters;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    internal class UpgradesRepository
    : GenericRepository<Upgrades>, IUpgradesRepository
    {
        private readonly MainContext _context;

        public UpgradesRepository(MainContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DoesUpgradeNameExists(string name, Guid? idToExclude)
        {
            return await PropertyExistsWithValue(e => e.Name, name, idToExclude.GetValueOrDefault());
        }

        public async Task<List<Upgrades>> ListUpgrades(UpgradesQueryFilter filter)
        {
            IQueryable<Upgrades> upgradeses = _context.Upgrades.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                upgradeses = upgradeses.Where(up => up.Name.Contains(filter.Name));
            }

            return await upgradeses.ToListAsync();
        }
    }
}