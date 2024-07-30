
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Properties>
    {
        Task<bool> IsCodeUnique(string code);
        Task<List<Properties>> GetPropertyByAgentId(Guid agentId);
        Task<Properties?> GetByIdWithPictures(Guid id);
    }
}