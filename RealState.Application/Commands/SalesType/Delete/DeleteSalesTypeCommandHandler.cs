
using MediatR;

using RealState.Application.Interfaces.Repositories;

namespace RealState.Application.Commands.SalesType.Delete
{
    internal class DeleteSalesTypeCommandHandler
    : IRequestHandler<DeleteSalesTypeCommand, Unit>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;

        public DeleteSalesTypeCommandHandler(ISalesTypeRepository salesTypeRepository)
        {
            _salesTypeRepository = salesTypeRepository;
        }

        public async Task<Unit> Handle(DeleteSalesTypeCommand request, CancellationToken cancellationToken)
        {
            await _salesTypeRepository.Delete(request.Id);
            return Unit.Value;
        }
    }
}