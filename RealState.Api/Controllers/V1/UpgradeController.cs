using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Commands.Upgrade.Create;
using RealState.Application.Commands.Upgrade.Delete;
using RealState.Application.Commands.Upgrade.Update;
using RealState.Application.DTOs.Upgrade;
using RealState.Application.Enums;
using RealState.Application.Queries.Upgrade.GetAll;
using RealState.Application.Queries.Upgrade.GetById;
using RealState.Application.QueryFilters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    [SwaggerTag("Operations related to upgrade management")]
    public class UpgradeController : ControllerBase
    {
        private readonly ISender _sender;

        public UpgradeController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Creates a new upgrade.
        /// </summary>
        /// <param name="cmd">The create upgrade command</param>
        /// <returns>No content on success</returns>
        [HttpPost("Upgrade")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(Summary = "Create a new upgrade", Description = "Creates a new upgrade. Requires Admin role.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Upgrade created successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> Post([FromBody] CreateUpgradeCommand cmd)
        {
            await _sender.Send(cmd);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing upgrade.
        /// </summary>
        /// <param name="cmd">The update upgrade command</param>
        /// <returns>The updated upgrade</returns>
        [HttpPut("Upgrade")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(Summary = "Update an upgrade", Description = "Updates an existing upgrade. Requires Admin role.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Upgrade updated successfully", typeof(UpgradeDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Upgrade not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> Put([FromBody] UpdateUpgradeCommand cmd)
        {
            return Ok(await _sender.Send(cmd));
        }

        /// <summary>
        /// Deletes an upgrade.
        /// </summary>
        /// <param name="id">The ID of the upgrade to delete</param>
        /// <returns>No content on success</returns>
        [HttpDelete("Upgrade")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(Summary = "Delete an upgrade", Description = "Deletes an existing upgrade. Requires Admin role.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Upgrade deleted successfully or not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            await _sender.Send(new DeleteUpgradeCommand() { Id = id });
            return NoContent();
        }

        /// <summary>
        /// Retrieves an upgrade by its ID.
        /// </summary>
        /// <param name="id">The ID of the upgrade</param>
        /// <returns>The upgrade details</returns>
        [HttpGet("Upgrade/{id:Guid}")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(Summary = "Get upgrade by ID", Description = "Retrieves details of a specific upgrade by its ID. Requires Admin or Developer role.")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Upgrade details retrieved successfully", typeof(UpgradeDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Upgrade not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID format")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            UpgradeDTO upgradeDTO = await _sender.Send(new GetByIdUpgradeQuery() { Id = id });
            return Ok(upgradeDTO);
        }

        /// <summary>
        /// Retrieves a list of upgrades based on the provided filter.
        /// </summary>
        /// <param name="filters">Filter parameters for the query</param>
        /// <returns>A list of upgrades matching the filter criteria</returns>
        [HttpGet("Upgrades")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(Summary = "Get all upgrades", Description = "Retrieves a list of all upgrades based on the provided filter. Requires Admin or Developer role.")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "List of upgrades retrieved successfully", typeof(List<UpgradeDTO>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No upgrades found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid filter parameters")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        public async Task<IActionResult> List([FromQuery] GetAllUpgradeQuery filters)
        {
            List<UpgradeDTO> upgradeDTOs = await _sender.Send(filters);
            return Ok(upgradeDTOs);
        }
    }
}