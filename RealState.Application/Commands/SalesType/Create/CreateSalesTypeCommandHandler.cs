
using AutoMapper;

using MediatR;

using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Commands.SalesType.Create
{
    internal class CreateSalesTypeCommandHandler
    : IRequestHandler<CreateSalesTypeCommand, Unit>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;

        public CreateSalesTypeCommandHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateSalesTypeCommand request, CancellationToken cancellationToken)
        {
            SalesTypes salesTypes = _mapper.Map<SalesTypes>(request);
            await _salesTypeRepository.Create(salesTypes);
            return Unit.Value;
        }
    }
}