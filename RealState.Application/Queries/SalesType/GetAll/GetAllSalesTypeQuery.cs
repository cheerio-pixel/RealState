using MediatR;

using RealState.Application.DTOs.SalesType;
using RealState.Application.QueryFilters;

namespace RealState.Application.Queries.SalesType.GetAll
{
    public class GetAllSalesTypeQuery
    : IRequest<List<SalesTypeDTO>>
    {
        public required SalesTypesQueryFilter Filters { get; set; }
    }
}