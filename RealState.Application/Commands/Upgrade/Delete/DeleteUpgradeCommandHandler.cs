
using MediatR;

using RealState.Application.Interfaces.Repositories;

namespace RealState.Application.Commands.Upgrade.Delete
{
    internal class DeleteUpgradeCommandHandler
    : IRequestHandler<DeleteUpgradeCommand, Unit>
    {
        private readonly IUpgradesRepository _upgradeRepository;

        public DeleteUpgradeCommandHandler(IUpgradesRepository upgradeRepository)
        {
            _upgradeRepository = upgradeRepository;
        }

        public async Task<Unit> Handle(DeleteUpgradeCommand request, CancellationToken cancellationToken)
        {
            await _upgradeRepository.Delete(request.Id);
            return Unit.Value;
        }
    }
}