using MediatR;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.SsalesType.Update
{
    public class UpdateSsalesTypeCommand
    : IRequest<UpdateSsalesTypeResponse>
    {
        [SwaggerParameter(Description = "Id of the SsalesType to Update")]
        public required Guid Id { get; set; }
        [SwaggerParameter(Description = "Name of the SsalesType to Update")]
        public required string Name { get; set; }
        [SwaggerParameter(Description = "Description of the SsalesType to Update")]
        public required string Description { get; set; }
    }
}