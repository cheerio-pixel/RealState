using MediatR;

using RealState.Application.DTOs.PropertyType;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.PropertyType.GetById
{
    public class GetByIdPropertyTypeQuery
    : IRequest<PropertyTypeDTO>
    {
        [SwaggerParameter(Description = "Id of the Property Type to query")]
        public Guid Id { get; set; }
    }
}