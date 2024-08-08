using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.DTOs.Property;
using RealState.Application.Enums;
using RealState.Application.Queries.Property.GetAll;
using RealState.Application.Queries.Property.GetByCode;
using RealState.Application.Queries.Property.GetById;
using RealState.Application.QueryFilters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    [SwaggerTag("Operations related to property management")]
    public class PropertyController : ControllerBase
    {
        private readonly ISender _sender;

        public PropertyController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Retrieves a property by its code.
        /// </summary>
        /// <param name="code">The code of the property</param>
        /// <returns>The property details</returns>
        [HttpGet("Code/{code}/Property")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(Summary = "Get property by code", Description = "Retrieves details of a specific property by its code. Requires Admin or Developer role.")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Property details retrieved successfully", typeof(PropertyDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Property not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid code format")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            PropertyDTO propertyDTO = await _sender.Send(new GetByCodeQuery()
            {
                Code = code
            });
            return Ok(propertyDTO);
        }

        /// <summary>
        /// Retrieves a property by its ID.
        /// </summary>
        /// <param name="id">The ID of the property</param>
        /// <returns>The property details</returns>
        [HttpGet("Property/{id:Guid}")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(Summary = "Get property by ID", Description = "Retrieves details of a specific property by its ID. Requires Admin or Developer role.")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Property details retrieved successfully", typeof(PropertyDTO))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Property not found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid ID format")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            PropertyDTO propertyDTO = await _sender.Send(new GetByIdPropertyQuery()
            {
                Id = id
            });
            return Ok(propertyDTO);
        }

        /// <summary>
        /// Retrieves a list of properties based on the provided filter.
        /// </summary>
        /// <param name="filters">Filter parameters for the query</param>
        /// <returns>A list of properties matching the filter criteria</returns>
        [HttpGet("Properties")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        [SwaggerOperation(Summary = "Get all properties", Description = "Retrieves a list of all properties based on the provided filter. Requires Admin or Developer role.")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "List of properties retrieved successfully", typeof(List<PropertyDTO>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No properties found")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid filter parameters")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have required role")]
        public async Task<IActionResult> List([FromQuery] GetAllPropertyQuery filters)
        {
            List<PropertyDTO> propertyDTOs = await _sender.Send(filters);
            return Ok(propertyDTOs);
        }
    }
}
