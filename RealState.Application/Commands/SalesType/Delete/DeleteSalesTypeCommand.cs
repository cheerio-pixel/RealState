using MediatR;

namespace RealState.Application.Commands.SalesType.Delete
{
    public class DeleteSalesTypeCommand
    : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}