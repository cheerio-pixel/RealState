
using AutoMapper;

using MediatR;

using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Commands.PropertyType.Create
{
    internal class CreatePropertyTypeCommandHandler
    : IRequestHandler<CreatePropertyTypeCommand, Unit>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public CreatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            PropertyTypes propertyTypes = _mapper.Map<PropertyTypes>(request);
            await _propertyTypeRepository.Create(propertyTypes);
            return Unit.Value;
        }
    }
}