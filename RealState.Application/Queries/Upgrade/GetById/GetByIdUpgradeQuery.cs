using MediatR;

using RealState.Application.DTOs.Upgrade;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.Upgrade.GetById
{
    public class GetByIdUpgradeQuery
    : IRequest<UpgradeDTO>
    {
        [SwaggerParameter(Description = "Id of the Upgrade to query")]
        public Guid Id { get; set; }
    }
}