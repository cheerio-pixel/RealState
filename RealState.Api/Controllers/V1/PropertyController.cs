using Asp.Versioning;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;

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
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            // PropertyDTO propertyTypeDTO = await _sender.Send(new GetByIdUpgradeQuery()
            // {
            //     Id = id
            // });
            // return Ok(propertyTypeDTO);
            throw new NotImplementedException();
        }

        [HttpGet("Property/{id:Guid}")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // PropertyDTO propertyTypeDTO = await _sender.Send(new GetByIdUpgradeQuery()
            // {
            //     Id = id
            // });
            // return Ok(propertyTypeDTO);
            throw new NotImplementedException();
        }

        [HttpGet("Properties")]
        [Authorize(Roles = nameof(RoleTypes.Admin) + "," + nameof(RoleTypes.Developer))]
        public async Task<IActionResult> List(/* [FromQuery] PropertysQueryFilter filters */)
        {
            // List<PropertyDTO> propertyTypeDTOs = await _sender.Send(new GetAllUpgradeQuery()
            // {
            //     Filters = filters
            // });
            // return Ok(propertyTypeDTOs);
            throw new NotImplementedException();
        }
    }
}