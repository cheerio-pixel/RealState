using MediatR;

using RealState.Application.DTOs.Property;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.Property.GetById
{
    public class GetByIdPropertyQuery
    : IRequest<PropertyDTO>
    {
        [SwaggerParameter(Description = "Id of the Property to query")]
        public required Guid Id { get; set; }
    }
}