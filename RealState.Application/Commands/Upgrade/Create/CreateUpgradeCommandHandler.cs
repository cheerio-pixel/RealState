
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Commands.Upgrade.Create
{
    internal class CreateUpgradeCommandHandler
    : IRequestHandler<CreateUpgradeCommand, Unit>
    {
        private readonly IUpgradesRepository _upgradeRepository;
        private readonly IMapper _mapper;

        public CreateUpgradeCommandHandler(IUpgradesRepository upgradeRepository, IMapper mapper)
        {
            _upgradeRepository = upgradeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUpgradeCommand request, CancellationToken cancellationToken)
        {
            if (await _upgradeRepository.DoesUpgradeNameExists(request.Name, Guid.Empty))
            {
                HttpStatusCode.Conflict
               .Because("Upgrade's name already exists.")
               .On(nameof(request.Name))
               .Throw();
            }
            Upgrades upgrades = _mapper.Map<Upgrades>(request);
            await _upgradeRepository.Create(upgrades);
            return Unit.Value;
        }
    }
}