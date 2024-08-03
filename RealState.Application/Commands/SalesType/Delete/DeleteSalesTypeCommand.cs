using MediatR;

namespace RealState.Application.Commands.SsalesType.Delete
{
    public class DeleteSsalesTypeCommand
    : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}