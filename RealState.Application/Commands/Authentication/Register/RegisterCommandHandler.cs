

using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.Account.RegisterResponse;
using RealState.Application.DTOs.User;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;

namespace RealState.Application.Commands.Authentication.Register
{
    internal class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, ApplicationUserDTO>
    {
        private readonly IIdentityServices _identityServices;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IIdentityServices identityServices, IMapper mapper, IRoleRepository roleRepository)
        {
            _identityServices = identityServices;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<ApplicationUserDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            SaveApplicationUserDTO saveApplicationUserDTO = _mapper.Map<SaveApplicationUserDTO>(request);
            saveApplicationUserDTO.Picture = PictureHelper.GetDefaultUserImage();
            saveApplicationUserDTO.Roles = [await _roleRepository.GetByNameAsync(request.Role.ToString())];
            RegisterResponseDTO registerResponseDTO
                = await _identityServices.RegisterAsync(saveApplicationUserDTO);

            if (!registerResponseDTO.Success)
            {
                HttpStatusCode
               .BadRequest
               .Because(registerResponseDTO.Error)
               .Throw();
            }
            return registerResponseDTO.NewUser!; // We did dhe checking
        }
    }
}