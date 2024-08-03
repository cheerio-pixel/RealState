using MediatR;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.SalesType.Update
{
    public class UpdateSalesTypeCommand
    : IRequest<UpdateSalesTypeResponse>
    {
        [SwaggerParameter(Description = "Id of the SalesType to Update")]
        public required Guid Id { get; set; }
        [SwaggerParameter(Description = "Name of the SalesType to Update")]
        public required string Name { get; set; }
        [SwaggerParameter(Description = "Description of the SalesType to Update")]
        public required string Description { get; set; }
    }
}