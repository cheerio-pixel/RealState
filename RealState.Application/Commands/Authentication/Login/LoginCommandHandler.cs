
using System.Net;

using MediatR;

using RealState.Application.DTOs.Account.Authentication;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Services;

namespace RealState.Application.Commands.Authentication.Login
{
    internal class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IIdentityServices _identityServices;

        public LoginCommandHandler(IIdentityServices identityServices)
        {
            _identityServices = identityServices;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            AuthenticationResponseDTO authenticationResponseDTO = await _identityServices.AuthenticationAsync(new AuthenticationRequestDTO()
            {
                Account = request.EmailOrUsername,
                Password = request.Password
            });
            if (!authenticationResponseDTO.Success)
            {
                HttpStatusCode
               .Unauthorized
               .Because(authenticationResponseDTO.Error!)
               .Throw();
            }

            return new()
            {
                CurrentUser = authenticationResponseDTO.CurrentUser,
                JWToken = authenticationResponseDTO.JWToken
            };
        }
    }
}