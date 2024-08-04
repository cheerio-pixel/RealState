using MediatR;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Commands.Authentication.Login;

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
    }
}