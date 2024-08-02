using MediatR;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.PropertyType.Update
{
    public class UpdatePropertyTypeCommand
    : IRequest<UpdatePropertyTypeResponse>
    {
        [SwaggerParameter(Description = "Id of the PropertyType to Update")]
        public required Guid Id { get; set; }
        [SwaggerParameter(Description = "Name of the PropertyType to Update")]
        public required string Name { get; set; }
        [SwaggerParameter(Description = "Description of the PropertyType to Update")]
        public required string Description { get; set; }
    }
}