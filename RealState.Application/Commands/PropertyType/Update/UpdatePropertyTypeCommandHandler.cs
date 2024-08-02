
using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Commands.PropertyType.Update
{
    internal class UpdatePropertyTypeCommandHandler
    : IRequestHandler<UpdatePropertyTypeCommand, UpdatePropertyTypeResponse>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public UpdatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<UpdatePropertyTypeResponse> Handle(UpdatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            PropertyTypes propertyToUpdate = _mapper.Map<PropertyTypes>(request);
            PropertyTypes? propertyTypes = await _propertyTypeRepository.Update(propertyToUpdate);
            if (propertyTypes is null)
            {
                HttpStatusCode
               .NoContent
               .Because($"We cannot find a property type with id {request.Id} does not exist ")
               .Throw();
            }
            return _mapper.Map<UpdatePropertyTypeResponse>(propertyTypes);
        }
    }
}