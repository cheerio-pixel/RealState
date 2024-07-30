using RealState.Application.DTOs.SalesType;
using RealState.Application.QueryFilters;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Repositories
{
    public interface ISalesTypeRepository
    : IGenericRepository<SalesTypes>
    {
        Task<bool> DoesSalesTypeNameExists(string name, Guid? idToExclude);
        Task<List<SalesTypesListItemDTO>> ListSalesTypes(SalesTypesQueryFilter filter);
    }
}