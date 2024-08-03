
using MediatR;

using RealState.Application.Interfaces.Repositories;

namespace RealState.Application.Commands.SsalesType.Delete
{
    internal class DeleteSsalesTypeCommandHandler
    : IRequestHandler<DeleteSsalesTypeCommand, Unit>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;

        public DeleteSsalesTypeCommandHandler(ISalesTypeRepository salesTypeRepository)
        {
            _salesTypeRepository = salesTypeRepository;
        }

        public async Task<Unit> Handle(DeleteSsalesTypeCommand request, CancellationToken cancellationToken)
        {
            await _salesTypeRepository.Delete(request.Id);
            return Unit.Value;
        }
    }
}