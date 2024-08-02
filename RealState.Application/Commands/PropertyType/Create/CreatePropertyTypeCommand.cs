using MediatR;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.PropertyType.Create
{
    public class CreatePropertyTypeCommand
    : IRequest<Unit>
    {
        [SwaggerParameter(Description = "Name of the PropertyType to Create")]
        public required string Name { get; set; }
        [SwaggerParameter(Description = "Description of the PropertyType to Create")]
        public required string Description { get; set; }
    }
}