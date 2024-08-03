using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Commands.Agent.ChangeStatus;
using RealState.Application.Queries.Agent.GetAll;
using RealState.Application.Queries.Agent.GetById;
using RealState.Application.Queries.Agent.GetPropertiesByAgent;
using RealState.Application.QueryFilters.User;

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly ISender _sender;

        public AgentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusAgentCommand cmd)
        {
            await _sender.Send(cmd);
            return NoContent();
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery]AgentQueryFilter filter)
        {
             var result = await _sender.Send(new GetAllAgentQuery() { Filter = filter});
            return Ok(result);
        }

        [HttpGet("Get{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            var result = await _sender.Send(new GetByIdAgentQuery() { AgentId = Id});
            return Ok(result);
        }

        [HttpGet("GetAgentProperties{Id}")]
        public async Task<IActionResult> GetAgentProperties(string Id)
        {
            var result = await _sender.Send(new GetPropertiesByAgentQuery() { AgentId = Id });
            return Ok(result);
        }
    }
}
