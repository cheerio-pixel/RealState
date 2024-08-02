using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.PropertyType;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Queries.PropertyType.GetById
{
    internal class GetByIdPropertyTypeQueryHandler
    : IRequestHandler<GetByIdPropertyTypeQuery, PropertyTypeDTO>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public GetByIdPropertyTypeQueryHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<PropertyTypeDTO> Handle(GetByIdPropertyTypeQuery request, CancellationToken cancellationToken)
        {
            PropertyTypes? propertyTypes = await _propertyTypeRepository.GetById(request.Id);
            if (propertyTypes is null)
            {
                HttpStatusCode
               .NoContent
               .Because($"We cannot find a property type with id {request.Id} does not exist ")
               .Throw();
            }
            return _mapper.Map<PropertyTypeDTO>(propertyTypes);
        }
    }
}