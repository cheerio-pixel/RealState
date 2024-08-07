using MediatR;
using RealState.Application.DTOs.PropertyType;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.PropertyType.GetAll
{
    public class GetAllPropertyTypeQuery : IRequest<List<PropertyTypeDTO>>
    {
        [SwaggerParameter(Description = "Optional filter by the name of the property type")]
        public string? Name { get; set; }
    }
}
