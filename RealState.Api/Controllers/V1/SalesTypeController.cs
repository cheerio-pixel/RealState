
using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Commands.SalesType.Create;
using RealState.Application.Commands.SalesType.Delete;
using RealState.Application.Commands.SalesType.Update;
using RealState.Application.DTOs.SalesType;
using RealState.Application.Queries.SalesType.GetAll;
using RealState.Application.Queries.SalesType.GetById;
using RealState.Application.QueryFilters;

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SalesTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public SalesTypeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("SalesType")]
        public async Task<IActionResult> Post([FromBody] CreateSalesTypeCommand cmd)
        {
            await _sender.Send(cmd);
            return NoContent();
        }

        [HttpPut("SalesType")]
        public async Task<IActionResult> Put([FromBody] UpdateSalesTypeCommand cmd)
        {
            return Ok(await _sender.Send(cmd));
        }

        [HttpDelete("SalesType")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            _ = await _sender.Send(new DeleteSalesTypeCommand()
            {
                Id = id
            });
            return NoContent();
        }

        [HttpGet("SalesType/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            SalesTypeDTO propertyTypeDTO = await _sender.Send(new GetByIdSalesTypeQuery()
            {
                Id = id
            });
            return Ok(propertyTypeDTO);
        }

        [HttpGet("SalesTypes")]
        public async Task<IActionResult> List([FromQuery] SalesTypesQueryFilter filters)
        {
            List<SalesTypeDTO> propertyTypeDTOs = await _sender.Send(new GetAllSalesTypeQuery()
            {
                Filters = filters
            });
            return Ok(propertyTypeDTOs);
        }
    }
}