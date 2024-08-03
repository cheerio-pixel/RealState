using MediatR;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.Upgrade.Update
{
    public class UpdateUpgradeCommand
    : IRequest<UpdateUpgradeResponse>
    {
        [SwaggerParameter(Description = "Id of the Upgrade to Update")]
        public required Guid Id { get; set; }
        [SwaggerParameter(Description = "Name of the Upgrade to Update")]
        public required string Name { get; set; }
        [SwaggerParameter(Description = "Description of the Upgrade to Update")]
        public required string Description { get; set; }
    }
}