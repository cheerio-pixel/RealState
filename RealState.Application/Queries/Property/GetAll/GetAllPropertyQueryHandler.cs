
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.Property;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.QueryFilters;
using RealState.Domain.Entities;

namespace RealState.Application.Queries.Property.GetAll
{
    public class GetAllPropertyQueryHandler
    : IRequestHandler<GetAllPropertyQuery, List<PropertyDTO>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAllPropertyQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyDTO>> Handle(GetAllPropertyQuery request, CancellationToken cancellationToken)
        {
            List<Properties> properties = await _propertyRepository.ListProperties(new PropertyQueryFilter()
            {
                Code = request.Code,
                Rooms = request.Rooms,
                Bathrooms = request.Bathrooms,
                PropertyTypeId = request.PropertyTypeId,
                AgentId = request.AgentId,
                MaxPrice = request.MaxPrice,
                MinPrice = request.MinPrice
            });
            if (properties.Count == 0)
            {
                HttpStatusCode
               .NoContent
               .Because("There is no content")
               .Throw();
            }
            return _mapper.Map<List<PropertyDTO>>(properties);
        }
    }
}