
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Properties>
    {
        Task<bool> IsCodeUnique(string code);
        Task<Properties?> GetByIdWithPictures(Guid id);
    }
}