
using RealState.Application.QueryFilters;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Properties>
    {
        Task<bool> IsCodeUnique(string code);
        Task<List<Properties>> GetPropertyByAgentId(Guid agentId);
        Task<Properties?> GetByIdWithPictures(Guid id);
        Task<List<Properties>> ListProperties(PropertyQueryFilter filter);
        Task<Properties?> GetByIdWithInclude(Guid id);
        Task<int> GetNumberOfPropertiesOfAgent(Guid agentId);
    }
}