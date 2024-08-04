using MediatR;

using RealState.Application.DTOs.Upgrade;
using RealState.Application.QueryFilters;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.Upgrade.GetAll
{
    public class GetAllUpgradeQuery
    : IRequest<List<UpgradeDTO>>
    {
        [SwaggerParameter("Filter to apply to search")]
        public required UpgradesQueryFilter Filters { get; set; }
    }
}