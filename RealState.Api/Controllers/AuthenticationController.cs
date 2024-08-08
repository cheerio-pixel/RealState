using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Commands.Authentication.Login;
using RealState.Application.Commands.Authentication.Register;
using RealState.Application.DTOs.User;
using RealState.Application.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Authentication system")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="cmd">The login command containing user credentials</param>
        /// <returns>A JWT token upon successful authentication</returns>
        [HttpPost("Login")]
        [SwaggerOperation(
            Summary = "User login",
            Description = "Authenticates a user and returns a JWT token."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Login successful", typeof(LoginCommandResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid credentials")]
        public async Task<IActionResult> Login([FromBody] LoginCommand cmd)
        {
            LoginCommandResponse loginCommandResponse = await _sender.Send(cmd);
            return Ok(loginCommandResponse);
        }

        /// <summary>
        /// Registers a new developer user.
        /// </summary>
        /// <param name="cmd">The registration command containing user details</param>
        /// <returns>The newly created user details</returns>
        [HttpPost("Register/Developer")]
        [SwaggerOperation(
            Summary = "Register a developer",
            Description = "Registers a new user with the Developer role"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Registration successful", typeof(ApplicationUserDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid registration data")]
        public async Task<IActionResult> RegisterDeveloper([FromBody] RegisterCommand cmd)
        {
            cmd.Role = RoleTypes.Developer;
            ApplicationUserDTO applicationUserDTO = await _sender.Send(cmd);
            return Ok(applicationUserDTO);
        }

        /// <summary>
        /// Registers a new administrator user. Requires Admin role.
        /// </summary>
        /// <param name="cmd">The registration command containing user details</param>
        /// <returns>The newly created user details</returns>
        [HttpPost("Register/Administrator")]
        [Authorize(Roles = nameof(RoleTypes.Admin))]
        [SwaggerOperation(
            Summary = "Register an administrator",
            Description = "Registers a new user with the Admin role. Requires Admin privileges."
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status200OK, "Registration successful", typeof(ApplicationUserDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid registration data")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden - User does not have Admin role")]
        public async Task<IActionResult> RegisterAdministrator([FromBody] RegisterCommand cmd)
        {
            cmd.Role = RoleTypes.Admin;
            ApplicationUserDTO applicationUserDTO = await _sender.Send(cmd);
            return Ok(applicationUserDTO);
        }
    }
}