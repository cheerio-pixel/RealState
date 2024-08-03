
using AutoMapper;

using MediatR;

using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Commands.SsalesType.Create
{
    internal class CreateSsalesTypeCommandHandler
    : IRequestHandler<CreateSsalesTypeCommand, Unit>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;

        public CreateSsalesTypeCommandHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateSsalesTypeCommand request, CancellationToken cancellationToken)
        {
            SalesTypes salesTypes = _mapper.Map<SalesTypes>(request);
            await _salesTypeRepository.Create(salesTypes);
            return Unit.Value;
        }
    }
}