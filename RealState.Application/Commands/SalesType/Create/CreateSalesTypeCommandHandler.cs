
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.Exceptions;
using RealState.Application.Extras;
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
            if (await _salesTypeRepository.DoesSalesTypeNameExists(request.Name, Guid.Empty))
            {
                HttpStatusCode.Conflict
               .Because("Sales type's name already exists.")
               .On(nameof(request.Name))
               .Throw();
            }
            SalesTypes salesTypes = _mapper.Map<SalesTypes>(request);
            await _salesTypeRepository.Create(salesTypes);
            return Unit.Value;
        }
    }
}