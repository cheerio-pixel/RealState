
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Commands.Upgrade.Update
{
    internal class UpdateUpgradeCommandHandler
    : IRequestHandler<UpdateUpgradeCommand, UpdateUpgradeResponse>
    {
        private readonly IUpgradesRepository _upgradeRepository;
        private readonly IMapper _mapper;

        public UpdateUpgradeCommandHandler(IUpgradesRepository upgradeRepository, IMapper mapper)
        {
            _upgradeRepository = upgradeRepository;
            _mapper = mapper;
        }

        public async Task<UpdateUpgradeResponse> Handle(UpdateUpgradeCommand request, CancellationToken cancellationToken)
        {
            if (await _upgradeRepository.DoesUpgradeNameExists(request.Name, request.Id))
            {
                HttpStatusCode.Conflict
               .Because("Upgrade's name already exists.")
               .On(nameof(request.Name))
               .Throw();
            }
            Upgrades propertyToUpdate = _mapper.Map<Upgrades>(request);
            Upgrades? upgrades = await _upgradeRepository.Update(propertyToUpdate);
            if (upgrades is null)
            {
                HttpStatusCode
               .NoContent
               .Because($"We cannot find a property type with id {request.Id} does not exist ")
               .Throw();
            }
            return _mapper.Map<UpdateUpgradeResponse>(upgrades);
        }
    }
}