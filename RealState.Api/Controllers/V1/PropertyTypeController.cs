using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Commands.PropertyType.Create;
using RealState.Application.Commands.PropertyType.Delete;
using RealState.Application.Commands.PropertyType.Update;
using RealState.Application.DTOs.PropertyType;
using RealState.Application.Queries.PropertyType.GetAll;
using RealState.Application.Queries.PropertyType.GetById;
using RealState.Application.QueryFilters;

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PropertyTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public PropertyTypeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("PropertyType")]
        public async Task<IActionResult> Post([FromBody] CreatePropertyTypeCommand cmd)
        {
            await _sender.Send(cmd);
            return NoContent();
        }

        [HttpPut("PropertyType")]
        public async Task<IActionResult> Put([FromBody] UpdatePropertyTypeCommand cmd)
        {
            return Ok(await _sender.Send(cmd));
        }

        [HttpDelete("PropertyType")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            _ = await _sender.Send(new DeletePropertyTypeCommand()
            {
                Id = id
            });
            return NoContent();
        }

        [HttpGet("PropertyType/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            PropertyTypeDTO propertyTypeDTO = await _sender.Send(new GetByIdPropertyTypeQuery()
            {
                Id = id
            });
            return Ok(propertyTypeDTO);
        }

        [HttpGet("PropertyTypes")]
        public async Task<IActionResult> List([FromQuery] PropertyTypeQueryFilter filters)
        {
            List<PropertyTypeDTO> propertyTypeDTOs = await _sender.Send(new GetAllPropertyTypeQuery()
            {
                Filters = filters
            });
            return Ok(propertyTypeDTOs);
        }
    }
}
