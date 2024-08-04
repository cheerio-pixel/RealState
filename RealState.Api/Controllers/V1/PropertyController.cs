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

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly ISender _sender;

        public PropertyController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("Code/{code}/Property/")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> GetByCode([FromRoute] GetByCodeQuery id)
        {
            PropertyDTO propertyTypeDTO = await _sender.Send(id);
            return Ok(propertyTypeDTO);
        }

        [HttpGet("Property/{id:Guid}")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> GetById([FromRoute] GetByIdPropertyQuery id)
        {
            PropertyDTO propertyTypeDTO = await _sender.Send(id);
            return Ok(propertyTypeDTO);
        }

        [HttpGet("Properties")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> List([FromQuery] PropertyQueryFilter filters)
        {
            List<PropertyDTO> propertyTypeDTOs = await _sender.Send(new GetAllPropertyQuery()
            {
                Filter = filters
            });
            return Ok(propertyTypeDTOs);
        }
    }
}