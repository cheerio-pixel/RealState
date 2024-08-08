using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Commands.SalesType.Create;
using RealState.Application.Commands.SalesType.Delete;
using RealState.Application.Commands.SalesType.Update;
using RealState.Application.DTOs.SalesType;
using RealState.Application.Enums;
using RealState.Application.Queries.SalesType.GetAll;
using RealState.Application.Queries.SalesType.GetById;
using RealState.Application.QueryFilters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    [SwaggerTag("Operations related to sales type management")]
    public class SalesTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public SalesTypeController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Creates a new sales type.
        /// </summary>
        /// <param name="cmd">The create sales type command</param>
        /// <returns>No content on success</returns>
        [HttpPost("SalesType")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(Summary = "Create a new sales type", Description = "Creates a new sales type. Requires Admin role.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Sales type created successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> Post([FromBody] CreateSalesTypeCommand cmd)
        {
            await _sender.Send(cmd);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing sales type.
        /// </summary>
        /// <param name="cmd">The update sales type command</param>
        /// <returns>The updated sales type</returns>
        [HttpPut("SalesType")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(Summary = "Update a sales type", Description = "Updates an existing sales type. Requires Admin role.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Sales type updated successfully", typeof(SalesTypeDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Sales type not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> Put([FromBody] UpdateSalesTypeCommand cmd)
        {
            return Ok(await _sender.Send(cmd));
        }

        /// <summary>
        /// Deletes a sales type.
        /// </summary>
        /// <param name="id">The ID of the sales type to delete</param>
        /// <returns>No content on success</returns>
        [HttpDelete("SalesType")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(Summary = "Delete a sales type", Description = "Deletes an existing sales type. Requires Admin role.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Sales type deleted successfully or not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            await _sender.Send(new DeleteSalesTypeCommand()
            {
                Id = id
            });
            return NoContent();
        }

        /// <summary>
        /// Retrieves a sales type by its ID.
        /// </summary>
        /// <param name="id">The ID of the sales type</param>
        /// <returns>The sales type details</returns>
        [HttpGet("SalesType/{id:Guid}")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(Summary = "Get sales type by ID", Description = "Retrieves details of a specific sales type by its ID. Requires Admin or Developer role.")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Sales type details retrieved successfully", typeof(SalesTypeDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Sales type not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID format")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            SalesTypeDTO salesTypeDTO = await _sender.Send(new GetByIdSalesTypeQuery()
            {
                Id = id
            });
            return Ok(salesTypeDTO);
        }

        /// <summary>
        /// Retrieves a list of sales types based on the provided filter.
        /// </summary>
        /// <param name="filters">Filter parameters for the query</param>
        /// <returns>A list of sales types matching the filter criteria</returns>
        [HttpGet("SalesTypes")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(Summary = "Get all sales types", Description = "Retrieves a list of all sales types based on the provided filter. Requires Admin or Developer role.")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "List of sales types retrieved successfully", typeof(List<SalesTypeDTO>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No sales types found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid filter parameters")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        public async Task<IActionResult> List([FromQuery] GetAllSalesTypeQuery filters)
        {
            List<SalesTypeDTO> salesTypeDTOs = await _sender.Send(filters);
            return Ok(salesTypeDTOs);
        }
    }
}