
using MediatR;

using RealState.Application.Interfaces.Repositories;

namespace RealState.Application.Commands.PropertyType.Delete
{
    internal class DeletePropertyTypeCommandHandler
    : IRequestHandler<DeletePropertyTypeCommand, Unit>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;

        public DeletePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository)
        {
            _propertyTypeRepository = propertyTypeRepository;
        }

        public async Task<Unit> Handle(DeletePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            await _propertyTypeRepository.Delete(request.Id);
            return Unit.Value;
        }
    }
}