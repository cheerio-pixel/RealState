using MediatR;

using RealState.Application.DTOs.PropertyType;
using RealState.Application.QueryFilters;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.PropertyType.GetAll
{
    public class GetAllPropertyTypeQuery
    : IRequest<List<PropertyTypeDTO>>
    {
        [SwaggerParameter("Filter to apply to search")]
        public required PropertyTypeQueryFilter Filters { get; set; }
    }
}