using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.Property;
using RealState.Application.DTOs.User;
using RealState.Application.Enums;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Queries.Agent.GetPropertiesByAgent
{
    public class GetPropertiesByAgentQueryHandler
        : IRequestHandler<GetPropertiesByAgentQuery, List<PropertyDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _propertyRepository;

        public GetPropertiesByAgentQueryHandler(IUserRepository userRepository, IMapper mapper, IPropertyRepository propertyRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }
        public async Task<List<PropertyDTO>> Handle(GetPropertiesByAgentQuery request, CancellationToken cancellationToken)
        {
            var agent = await _userRepository.GetWithInclude(request.AgentId, ["Roles"]);
            if (agent is null || !agent.Roles.Any(x => x.Name == RoleTypes.StateAgent.ToString()))
                HttpStatusCode
                .NoContent
                .Because("There is no content")
                .Throw();

            var properties = await _propertyRepository.GetPropertyByAgentId(Guid.Parse(request.AgentId));
            if(!properties.Any())
                HttpStatusCode
                .NoContent
                .Because("There is no content")
                .Throw();

            return _mapper.Map<List<PropertyDTO>>(properties);
        }
    }
}
