using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.Upgrades;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Services
{
    public interface IUpgradesService
    : IGenericService<UpgradesSaveViewModel, UpgradesViewModel, Upgrades>
    {
        Task<List<UpgradesViewModel>> SearchUpgrades(UpgradesQueryFilter filter);
    }
}