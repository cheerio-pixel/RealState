using MediatR;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.SsalesType.Create
{
    public class CreateSsalesTypeCommand
    : IRequest<Unit>
    {
        [SwaggerParameter(Description = "Name of the SsalesType to Create")]
        public required string Name { get; set; }
        [SwaggerParameter(Description = "Description of the SsalesType to Create")]
        public required string Description { get; set; }
    }
}