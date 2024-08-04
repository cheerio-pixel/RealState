using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Commands.Authentication.Login;
using RealState.Application.DTOs.Account.Authentication;

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