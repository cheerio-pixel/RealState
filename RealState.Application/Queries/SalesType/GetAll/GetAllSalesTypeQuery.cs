using MediatR;

using RealState.Application.DTOs.SalesType;
using RealState.Application.QueryFilters;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.SalesType.GetAll
{
    public class GetAllSalesTypeQuery
    : IRequest<List<SalesTypeDTO>>
    {
        [SwaggerParameter("Filter to apply to search")]
        public required SalesTypesQueryFilter Filters { get; set; }
    }
}