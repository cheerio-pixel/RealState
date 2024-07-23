using AutoMapper;
using MediatR;
using RealState.Application.Dtos;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.ViewModel.Property;
using RealState.Domain.Entities;


namespace RealState.Application.Commands.Property.Create
{
    public class CreatePropertyCommand : IRequest<TResult<PropertyViewModel>>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string Code { get; set; } = null!;
        public decimal Meters { get; set; }
        public List<string> Pictures { get; set; } = null!;
        public Guid PropertyTypeId { get; set; }
        public Guid SalesTypeId { get; set; }
        public Guid UpgradeId { get; set; }

    }

    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, TResult<PropertyViewModel>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<TResult<PropertyViewModel>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Properties>(request);

            var code = UniqueCodeGenerator.UniqueCode();

            while (!await _propertyRepository.IsCodeUnique(code))
            {
                code = UniqueCodeGenerator.UniqueCode();
            }
            property.Code = code;

            var propertyCreated = await _propertyRepository.Create(property);

            return TResult<PropertyViewModel>.Success(_mapper.Map<PropertyViewModel>(propertyCreated));
        }
    }
}