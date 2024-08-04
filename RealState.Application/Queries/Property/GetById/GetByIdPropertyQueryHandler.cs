
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.Property;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Queries.Property.GetById
{
    internal class GetByIdPropertyQueryHandler
    : IRequestHandler<GetByIdPropertyQuery, PropertyDTO>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetByIdPropertyQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<PropertyDTO> Handle(GetByIdPropertyQuery request, CancellationToken cancellationToken)
        {
            Properties? property = await _propertyRepository.GetById(request.Id);
            if (property is null)
            {
                HttpStatusCode
               .NoContent
               .Because($"We cannot find a property with id {request.Id} does not exist ")
               .Throw();
            }
            return _mapper.Map<PropertyDTO>(property);
        }
    }
}