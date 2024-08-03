using MediatR;

using RealState.Application.DTOs.PropertyType;
using RealState.Application.QueryFilters;

namespace RealState.Application.Queries.PropertyType.GetAll
{
    public class GetAllPropertyTypeQuery
    : IRequest<List<PropertyTypeDTO>>
    {
        public required PropertyTypeQueryFilter Filters { get; set; }
    }
}