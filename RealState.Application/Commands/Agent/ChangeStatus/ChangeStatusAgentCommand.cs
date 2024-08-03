using MediatR;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.Agent.ChangeStatus
{
    public class ChangeStatusAgentCommand 
        : IRequest<Unit>
    {
        [SwaggerParameter("Id of agent to change the status")]
        public required string AgentId { get; set; }

        [SwaggerParameter("the new status to the agent")]
        public required bool Status { get; set; }
    }
}
