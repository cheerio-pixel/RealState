using FluentValidation;

namespace RealState.Application.Commands.Agent.ChangeStatus
{
    public class ChangeStatusAgentCommandValidator
       : AbstractValidator<ChangeStatusAgentCommand>
    {
        public ChangeStatusAgentCommandValidator()
        {
            RuleFor(x => x.AgentId)
                .NotNull()
                .NotEmpty()
                .WithMessage("The agent id is required");

            RuleFor(x => x.Status)
                .NotNull()
                .NotEmpty()
                .WithMessage("The status is required");
        }
    }
}
