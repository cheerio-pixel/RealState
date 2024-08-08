using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Commands.Agent.ChangeStatus;
using RealState.Application.DTOs.Property;
using RealState.Application.DTOs.User;
using RealState.Application.Enums;
using RealState.Application.Queries.Agent.GetAll;
using RealState.Application.Queries.Agent.GetById;
using RealState.Application.Queries.Agent.GetPropertiesByAgent;
using RealState.Application.QueryFilters.User;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    [SwaggerTag("Operations related to agent management")]
    public class AgentController : ControllerBase
    {
        private readonly ISender _sender;

        public AgentController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Changes the status of an agent.
        /// </summary>
        /// <param name="id">The ID of the agent</param>
        /// <param name="status">The new status of the agent</param>
        /// <returns>No content on success</returns>
        [HttpPatch("Agent/{id}/Status")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(
            Summary = "Change agent status",
            Description = "Changes the active status of an agent. Requires Admin role."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Status changed successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Agent not found")]
        public async Task<IActionResult> ChangeStatus([FromRoute] string id, [FromQuery] bool status)
        {
            await _sender.Send(new ChangeStatusAgentCommand()
            {
                AgentId = id,
                Status = status
            });
            return NoContent();
        }

        /// <summary>
        /// Retrieves a list of agents based on the provided filter.
        /// </summary>
        /// <param name="filter">Filter parameters for the query</param>
        /// <returns>A list of agents matching the filter criteria</returns>
        [HttpGet("Agents")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(
            Summary = "Get all agents",
            Description = "Retrieves a list of all agents based on the provided filter. Requires Admin or Developer role."
        )]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "List of agents retrieved successfully", typeof(List<AgentDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid filter parameters")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No agents present")]
        public async Task<IActionResult> Get([FromQuery] GetAllAgentQuery filter)
        {
            List<AgentDTO> result = await _sender.Send(filter);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves an agent by their ID.
        /// </summary>
        /// <param name="id">The ID of the agent</param>
        /// <returns>The agent details</returns>
        [HttpGet("Agent/{id}")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(
            Summary = "Get agent by ID",
            Description = "Retrieves details of a specific agent by their ID. Requires Admin or Developer role."
        )]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Agent details retrieved successfully", typeof(AgentDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID format")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Agent not found")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            AgentDTO result = await _sender.Send(new GetByIdAgentQuery() { AgentId = id });
            return Ok(result);
        }

        /// <summary>
        /// Retrieves properties associated with a specific agent.
        /// </summary>
        /// <param name="id">The ID of the agent</param>
        /// <returns>A list of properties associated with the agent</returns>
        [HttpGet("Agent/{id}/Properties")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(
            Summary = "Get properties by agent",
            Description = "Retrieves a list of properties associated with a specific agent. Requires Admin or Developer role."
        )]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Properties retrieved successfully", typeof(List<PropertyDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID format")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Agent or properties not found")]
        public async Task<IActionResult> GetAgentProperties(string id)
        {
            List<PropertyDTO> result = await _sender.Send(new GetPropertiesByAgentQuery() { AgentId = id });
            return Ok(result);
        }
    }
}
