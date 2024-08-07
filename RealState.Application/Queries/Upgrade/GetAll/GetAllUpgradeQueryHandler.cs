
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.Upgrade;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
namespace RealState.Application.Queries.Upgrade.GetAll
{
    internal class GetAllUpgradeQueryHandler
    : IRequestHandler<GetAllUpgradeQuery, List<UpgradeDTO>>
    {
        private readonly IUpgradesRepository _upgradeRepository;
        private readonly IMapper _mapper;

        public GetAllUpgradeQueryHandler(IUpgradesRepository upgradeRepository, IMapper mapper)
        {
            _upgradeRepository = upgradeRepository;
            _mapper = mapper;
        }

        public async Task<List<UpgradeDTO>> Handle(GetAllUpgradeQuery request, CancellationToken cancellationToken)
        {
            List<Upgrades> upgrades = await _upgradeRepository.ListUpgrades(new()
            {
                Name = request.Name
            });
            if (upgrades.Count == 0)
            {
                HttpStatusCode
               .NoContent
               .Because("There is no content")
               .Throw();
            }
            return _mapper.Map<List<UpgradeDTO>>(upgrades);
        }
    }
}