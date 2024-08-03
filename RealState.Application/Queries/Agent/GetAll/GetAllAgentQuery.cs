using MediatR;

using RealState.Application.DTOs.User;
using RealState.Application.QueryFilters.User;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.Agent.GetAll
{
    public class GetAllAgentQuery
        : IRequest<List<AgentDTO>>
    {
        [SwaggerParameter("Filters to list of agents")]
        public required UserQueryFilter Filter { get; set; }
    }
}
