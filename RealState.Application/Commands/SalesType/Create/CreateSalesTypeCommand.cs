using MediatR;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.SalesType.Create
{
    public class CreateSalesTypeCommand
    : IRequest<Unit>
    {
        [SwaggerParameter(Description = "Name of the SalesType to Create")]
        public required string Name { get; set; }
        [SwaggerParameter(Description = "Description of the SalesType to Create")]
        public required string Description { get; set; }
    }
}