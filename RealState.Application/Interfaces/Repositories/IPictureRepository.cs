using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IPictureRepository : IGenericRepository<Pictures>
    {
        Task<List<Pictures>> GetAllByPropertyId(Guid propertyId);
    }
}