using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IPropertyUpgradeRepository : IGenericRepository<PropertiesUpgrades>
    {
        Task<List<PropertiesUpgrades>> GetAllByPropertyId(Guid propertyId);
    }
}