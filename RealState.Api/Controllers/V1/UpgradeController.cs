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

namespace RealState.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UpgradeController : ControllerBase
    {
        private readonly ISender _sender;

        public UpgradeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Upgrade")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        public async Task<IActionResult> Post([FromBody] CreateUpgradeCommand cmd)
        {
            await _sender.Send(cmd);
            return NoContent();
        }

        [HttpPut("Upgrade")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        public async Task<IActionResult> Put([FromBody] UpdateUpgradeCommand cmd)
        {
            return Ok(await _sender.Send(cmd));
        }

        [HttpDelete("Upgrade")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            _ = await _sender.Send(new DeleteUpgradeCommand()
            {
                Id = id
            });
            return NoContent();
        }

        [HttpGet("Upgrade/{id:Guid}")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            UpgradeDTO propertyTypeDTO = await _sender.Send(new GetByIdUpgradeQuery()
            {
                Id = id
            });
            return Ok(propertyTypeDTO);
        }

        [HttpGet("Upgrades")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> List([FromQuery] UpgradesQueryFilter filters)
        {
            List<UpgradeDTO> propertyTypeDTOs = await _sender.Send(new GetAllUpgradeQuery()
            {
                Filters = filters
            });
            return Ok(propertyTypeDTOs);
        }
    }
}