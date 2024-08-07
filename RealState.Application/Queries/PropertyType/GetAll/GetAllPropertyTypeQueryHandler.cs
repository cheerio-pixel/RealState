
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.PropertyType;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
namespace RealState.Application.Queries.PropertyType.GetAll
{
    internal class GetAllPropertyTypeQueryHandler
    : IRequestHandler<GetAllPropertyTypeQuery, List<PropertyTypeDTO>>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public GetAllPropertyTypeQueryHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyTypeDTO>> Handle(GetAllPropertyTypeQuery request, CancellationToken cancellationToken)
        {
            List<PropertyTypes> propertyTypes = await _propertyTypeRepository.ListPropertyTypes(new()
            {
                Name = request.Name
            });
            if (propertyTypes.Count == 0)
            {
                HttpStatusCode
               .NoContent
               .Because("There is no content")
               .Throw();
            }
            return _mapper.Map<List<PropertyTypeDTO>>(propertyTypes);
        }
    }
}