using MediatR;

using RealState.Application.DTOs.User;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.Agent.GetById
{
    public class GetByIdAgentQuery
        : IRequest<AgentDTO>
    {
        [SwaggerParameter("The agent id")]
        public required string AgentId { get; set; }
    }
}
