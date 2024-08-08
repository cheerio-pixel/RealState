
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Commands.SalesType.Update
{
    internal class UpdateSalesTypeCommandHandler
    : IRequestHandler<UpdateSalesTypeCommand, UpdateSalesTypeResponse>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;

        public UpdateSalesTypeCommandHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSalesTypeResponse> Handle(UpdateSalesTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _salesTypeRepository.DoesSalesTypeNameExists(request.Name, request.Id))
            {
                HttpStatusCode.Conflict
               .Because("Sales type's name already exists.")
               .On(nameof(request.Name))
               .Throw();
            }
            SalesTypes propertyToUpdate = _mapper.Map<SalesTypes>(request);
            SalesTypes? salesTypes = await _salesTypeRepository.Update(propertyToUpdate);
            if (salesTypes is null)
            {
                HttpStatusCode
               .NoContent
               .Because($"We cannot find a property type with id {request.Id} does not exist ")
               .Throw();
            }
            return _mapper.Map<UpdateSalesTypeResponse>(salesTypes);
        }
    }
}