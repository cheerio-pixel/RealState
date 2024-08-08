using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Commands.PropertyType.Create;
using RealState.Application.Commands.PropertyType.Delete;
using RealState.Application.Commands.PropertyType.Update;
using RealState.Application.DTOs.PropertyType;
using RealState.Application.Enums;
using RealState.Application.Queries.PropertyType.GetAll;
using RealState.Application.Queries.PropertyType.GetById;
using RealState.Application.QueryFilters;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mime;

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    [SwaggerTag("Operations related to property type management")]
    public class PropertyTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public PropertyTypeController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Creates a new property type.
        /// </summary>
        /// <param name="cmd">The create property type command</param>
        /// <returns>No content on success</returns>
        [HttpPost("PropertyType")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(Summary = "Create a new property type", Description = "Creates a new property type. Requires Admin role.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Property type created successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> Post([FromBody] CreatePropertyTypeCommand cmd)
        {
            await _sender.Send(cmd);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing property type.
        /// </summary>
        /// <param name="cmd">The update property type command</param>
        /// <returns>The updated property type</returns>
        [HttpPut("PropertyType")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(Summary = "Update a property type", Description = "Updates an existing property type. Requires Admin role.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Property type updated successfully", typeof(PropertyTypeDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Property type not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> Put([FromBody] UpdatePropertyTypeCommand cmd)
        {
            return Ok(await _sender.Send(cmd));
        }

        /// <summary>
        /// Deletes a property type.
        /// </summary>
        /// <param name="id">The ID of the property type to delete</param>
        /// <returns>No content on success</returns>
        [HttpDelete("PropertyType")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(Summary = "Delete a property type", Description = "Deletes an existing property type. Requires Admin role.")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Property type deleted successfully or not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            _ = await _sender.Send(new DeletePropertyTypeCommand()
            {
                Id = id
            });
            return NoContent();
        }

        /// <summary>
        /// Retrieves a property type by its ID.
        /// </summary>
        /// <param name="id">The ID of the property type</param>
        /// <returns>The property type details</returns>
        [HttpGet("PropertyType/{id:Guid}")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(Summary = "Get property type by ID", Description = "Retrieves details of a specific property type by its ID. Requires Admin or Developer role.")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Property type details retrieved successfully", typeof(PropertyTypeDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Property type not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID format")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            PropertyTypeDTO propertyTypeDTO = await _sender.Send(new GetByIdPropertyTypeQuery()
            {
                Id = id
            });
            return Ok(propertyTypeDTO);
        }

        /// <summary>
        /// Retrieves a list of property types based on the provided filter.
        /// </summary>
        /// <param name="filters">Filter parameters for the query</param>
        /// <returns>A list of property types matching the filter criteria</returns>
        [HttpGet("PropertyTypes")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(Summary = "Get all property types", Description = "Retrieves a list of all property types based on the provided filter. Requires Admin or Developer role.")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "List of property types retrieved successfully", typeof(List<PropertyTypeDTO>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No property types found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid filter parameters")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        public async Task<IActionResult> List([FromQuery] GetAllPropertyTypeQuery filters)
        {
            List<PropertyTypeDTO> propertyTypeDTOs = await _sender.Send(filters);
            return Ok(propertyTypeDTOs);
        }
    }
}
