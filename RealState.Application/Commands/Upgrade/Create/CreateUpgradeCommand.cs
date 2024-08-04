using MediatR;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.Upgrade.Create
{
    public class CreateUpgradeCommand
    : IRequest<Unit>
    {
        [SwaggerParameter(Description = "Name of the Upgrade to Create")]
        public required string Name { get; set; }
        [SwaggerParameter(Description = "Description of the Upgrade to Create")]
        public required string Description { get; set; }
    }
}