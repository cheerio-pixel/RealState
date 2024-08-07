using MediatR;

using RealState.Application.DTOs.Upgrade;
using RealState.Application.QueryFilters;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.Upgrade.GetAll
{
    public class GetAllUpgradeQuery
    : IRequest<List<UpgradeDTO>>
    {
        [SwaggerParameter(Description = "Optional filter by the name of the upgrade.")]
        public string? Name { get; set; }
    }
}