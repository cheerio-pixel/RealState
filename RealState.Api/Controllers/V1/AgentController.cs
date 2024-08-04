using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Commands.Agent.ChangeStatus;
using RealState.Application.Enums;
using RealState.Application.Queries.Agent.GetAll;
using RealState.Application.Queries.Agent.GetById;
using RealState.Application.Queries.Agent.GetPropertiesByAgent;
using RealState.Application.QueryFilters.User;

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly ISender _sender;

        public AgentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPatch("Agent/{id}/Status")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        public async Task<IActionResult> ChangeStatus([FromRoute] string id, bool status)
        {
            await _sender.Send(new ChangeStatusAgentCommand()
            {
                AgentId = id,
                Status = status
            });
            return NoContent();
        }

        [HttpGet("Agents")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> Get([FromQuery] AgentQueryFilter filter)
        {
            var result = await _sender.Send(new GetAllAgentQuery() { Filter = filter });
            return Ok(result);
        }

        [HttpGet("Agent/{id}")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var result = await _sender.Send(new GetByIdAgentQuery() { AgentId = id });
            return Ok(result);
        }

        [HttpGet("Agent/{id}/Properties")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> GetAgentProperties(string id)
        {
            var result = await _sender.Send(new GetPropertiesByAgentQuery() { AgentId = id });
            return Ok(result);
        }
    }
}
