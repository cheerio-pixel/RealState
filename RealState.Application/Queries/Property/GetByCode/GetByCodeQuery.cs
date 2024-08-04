using MediatR;

using RealState.Application.DTOs.Property;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.Property.GetByCode
{
    public class GetByCodeQuery
    : IRequest<PropertyDTO>
    {
        [SwaggerParameter(Description = "Code of the Property to query")]
        public required string Code { get; set; }
    }
}