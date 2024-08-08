
using RealState.Application.QueryFilters;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Properties>
    {
        Task<bool> IsCodeUnique(string code);
        Task<List<Properties>> GetPropertyByAgentId(Guid agentId);
        Task<IEnumerable<Guid>> GetPropertyIdsByAgentId(Guid agentId);
        Task<Properties?> GetByIdWithPictures(Guid id);
        Task<List<Properties>> ListProperties(PropertyQueryFilter filter);
        Task<Properties?> GetByIdWithInclude(Guid id);
        Task<Properties?> GetByCode(string code);
        Task<int> GetNumberOfPropertiesOfAgent(Guid agentId);
        Task<List<Properties>> GetPropertyByAgentIdWithInclude(Guid agentId);
        Task<int> GetPropertyCount();
    }
}