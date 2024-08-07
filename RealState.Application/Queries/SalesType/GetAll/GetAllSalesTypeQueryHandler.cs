
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.SalesType;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
namespace RealState.Application.Queries.SalesType.GetAll
{
    internal class GetAllSalesTypeQueryHandler
    : IRequestHandler<GetAllSalesTypeQuery, List<SalesTypeDTO>>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;

        public GetAllSalesTypeQueryHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<SalesTypeDTO>> Handle(GetAllSalesTypeQuery request, CancellationToken cancellationToken)
        {
            List<SalesTypes> salesTypes = await _salesTypeRepository.ListSalesTypes(new()
            {
                Name = request.Name
            });
            if (salesTypes.Count == 0)
            {
                HttpStatusCode
               .NoContent
               .Because("There is no content")
               .Throw();
            }
            return _mapper.Map<List<SalesTypeDTO>>(salesTypes);
        }
    }
}