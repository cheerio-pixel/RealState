
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Properties>
    {
        Task<bool> IsCodeUnique(string code);
        Task<List<Properties>> GetPropertyByAgentId(Guid agentId);
        Task<IEnumerable<Guid>> GetPropertyIdsByAgentId(Guid agentId);
        Task<Properties?> GetByIdWithPictures(Guid id);
        Task<int> GetNumberOfPropertiesOfAgent(Guid agentId);
    }
}