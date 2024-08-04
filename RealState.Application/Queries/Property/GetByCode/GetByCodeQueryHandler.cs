
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.Property;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Queries.Property.GetByCode
{
    internal class GetByCodeQueryHandler
    : IRequestHandler<GetByCodeQuery, PropertyDTO>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetByCodeQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<PropertyDTO> Handle(GetByCodeQuery request, CancellationToken cancellationToken)
        {
            Properties? property = await _propertyRepository.GetByCode(request.Code);
            if (property is null)
            {
                HttpStatusCode
               .NoContent
               .Because($"We cannot find a property with id {request.Code} does not exist ")
               .Throw();
            }
            return _mapper.Map<PropertyDTO>(property);
        }
    }
}