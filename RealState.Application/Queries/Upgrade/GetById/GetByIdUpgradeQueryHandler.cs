using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.Upgrade;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Queries.Upgrade.GetById
{
    internal class GetByIdUpgradeQueryHandler
    : IRequestHandler<GetByIdUpgradeQuery, UpgradeDTO>
    {
        private readonly IUpgradesRepository _upgradeRepository;
        private readonly IMapper _mapper;

        public GetByIdUpgradeQueryHandler(IUpgradesRepository upgradeRepository, IMapper mapper)
        {
            _upgradeRepository = upgradeRepository;
            _mapper = mapper;
        }

        public async Task<UpgradeDTO> Handle(GetByIdUpgradeQuery request, CancellationToken cancellationToken)
        {
            Upgrades? upgrades = await _upgradeRepository.GetById(request.Id);
            if (upgrades is null)
            {
                HttpStatusCode
               .NoContent
               .Because($"We cannot find a property type with id {request.Id} does not exist ")
               .Throw();
            }
            return _mapper.Map<UpgradeDTO>(upgrades);
        }
    }
}