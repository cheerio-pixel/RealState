using RealState.Application.QueryFilters;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IUpgradesRepository
    : IGenericRepository<Upgrades>
    {
        Task<bool> DoesUpgradeNameExists(string name, Guid? idToExclude);
        Task<List<Upgrades>> ListUpgrades(UpgradesQueryFilter filter);
    }
}