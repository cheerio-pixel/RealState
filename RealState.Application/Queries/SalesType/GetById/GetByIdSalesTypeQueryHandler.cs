using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.SalesType;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Queries.SalesType.GetById
{
    internal class GetByIdSalesTypeQueryHandler
    : IRequestHandler<GetByIdSalesTypeQuery, SalesTypeDTO>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;

        public GetByIdSalesTypeQueryHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<SalesTypeDTO> Handle(GetByIdSalesTypeQuery request, CancellationToken cancellationToken)
        {
            SalesTypes? salesTypes = await _salesTypeRepository.GetById(request.Id);
            if (salesTypes is null)
            {
                HttpStatusCode
               .NoContent
               .Because($"We cannot find a property type with id {request.Id} does not exist ")
               .Throw();
            }
            return _mapper.Map<SalesTypeDTO>(salesTypes);
        }
    }
}