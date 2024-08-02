using MediatR;

namespace RealState.Application.Commands.PropertyType.Delete
{
    public class DeletePropertyTypeCommand
    : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}