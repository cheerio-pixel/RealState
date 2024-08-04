using MediatR;

namespace RealState.Application.Commands.Upgrade.Delete
{
    public class DeleteUpgradeCommand
    : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}