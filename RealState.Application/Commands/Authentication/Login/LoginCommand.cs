using MediatR;

using RealState.Application.DTOs.Account.Authentication;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Commands.Authentication.Login
{
    public class LoginCommand
    : IRequest<LoginCommandResponse>
    {
        [SwaggerParameter("Email or username of the user to log in")]
        public required string EmailOrUsername { get; set; }

        [SwaggerParameter("Password of the user")]
        public required string Password { get; set; }
    }
}