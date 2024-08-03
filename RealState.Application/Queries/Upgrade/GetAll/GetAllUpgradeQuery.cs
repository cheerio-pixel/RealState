using MediatR;

using RealState.Application.DTOs.Upgrade;
using RealState.Application.QueryFilters;

namespace RealState.Application.Queries.Upgrade.GetAll
{
    public class GetAllUpgradeQuery
    : IRequest<List<UpgradeDTO>>
    {
        public required UpgradesQueryFilter Filters { get; set; }
    }
}