using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.SalesType;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Services
{
    public interface ISalesTypesService
    : IGenericService<SalesTypeSaveViewModel, SalesTypeViewModel, SalesTypes>
    {
        Task<List<SalesTypeListItemViewModel>> SearchSalesTypes(SalesTypesQueryFilter filter);
    }
}