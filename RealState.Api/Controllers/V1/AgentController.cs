using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Commands.Agent.ChangeStatus;

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

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusAgentCommand cmd)
        {
            await _sender.Send(cmd);
            return NoContent();
        }
    }
}
