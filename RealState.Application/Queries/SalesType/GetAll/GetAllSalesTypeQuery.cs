using MediatR;

using RealState.Application.DTOs.SalesType;
using RealState.Application.QueryFilters;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.SalesType.GetAll
{
    public class GetAllSalesTypeQuery
    : IRequest<List<SalesTypeDTO>>
    {
        [SwaggerParameter(Description = "Optional filter by the name of the sales type.")]
        public string? Name { get; set; }
    }
}