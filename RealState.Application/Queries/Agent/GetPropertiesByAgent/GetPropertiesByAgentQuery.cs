using MediatR;

using RealState.Application.DTOs.Property;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.Agent.GetPropertiesByAgent
{
    public class GetPropertiesByAgentQuery
         : IRequest<List<PropertyDTO>>
    {
        [SwaggerParameter("The agent id")]
        public required string AgentId { get; set; }
    }
}
