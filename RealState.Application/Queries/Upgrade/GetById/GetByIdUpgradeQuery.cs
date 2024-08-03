using MediatR;

using RealState.Application.DTOs.Upgrade;

namespace RealState.Application.Queries.Upgrade.GetById
{
    public class GetByIdUpgradeQuery
    : IRequest<UpgradeDTO>
    {
        public Guid Id { get; set; }
    }
}