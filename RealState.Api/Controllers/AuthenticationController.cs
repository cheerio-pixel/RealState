using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Commands.Authentication.Login;
using RealState.Application.Commands.Authentication.Register;
using RealState.Application.DTOs.User;
using RealState.Application.Enums;

namespace RealState.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand cmd)
        {
            LoginCommandResponse loginCommandResponse = await _sender.Send(cmd);
            return Ok(loginCommandResponse);
        }

        [HttpPost("Register/Developer")]
        public async Task<IActionResult> RegisterDeveloper(RegisterCommand cmd)
        {
            cmd.Role = RoleTypes.Developer;
            ApplicationUserDTO applicationUserDTO = await _sender.Send(
                cmd
            );
            return Ok(applicationUserDTO);
        }

        [HttpPost("Register/Administrator")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        public async Task<IActionResult> RegisterAdministrator(RegisterCommand cmd)
        {
            cmd.Role = RoleTypes.Admin;
            ApplicationUserDTO applicationUserDTO = await _sender.Send(
                cmd
            );
            return Ok(applicationUserDTO);
        }
    }
}