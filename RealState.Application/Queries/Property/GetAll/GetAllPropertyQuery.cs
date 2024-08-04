using MediatR;

using RealState.Application.DTOs.Property;
using RealState.Application.QueryFilters;

namespace RealState.Application.Queries.Property.GetAll
{
    public class GetAllPropertyQuery
    : IRequest<List<PropertyDTO>>
    {
        public required PropertyQueryFilter Filter { get; set; }
    }
}