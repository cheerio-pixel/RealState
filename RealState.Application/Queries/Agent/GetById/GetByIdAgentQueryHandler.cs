using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.User;
using RealState.Application.Enums;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;

namespace RealState.Application.Queries.Agent.GetById
{
    public class GetByIdAgentQueryHandler
        : IRequestHandler<GetByIdAgentQuery, AgentDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetByIdAgentQueryHandler(IUserRepository userRepository, IMapper mapper, IPropertyRepository propertyRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }
        public async Task<AgentDTO> Handle(GetByIdAgentQuery request, CancellationToken cancellationToken)
        {
            ApplicationUserDTO? agent = await _userRepository.GetWithInclude(request.AgentId, ["Roles"]);
            if(agent is null || !agent.Roles.Any(x=>x.Name == RoleTypes.StateAgent.ToString()))
                HttpStatusCode
                .NoContent
                .Because("There is no content")
                .Throw();

            AgentDTO agentDto = _mapper.Map<AgentDTO>(agent);

            Guid agentId = Guid.Parse(agent!.Id); // Cannot be null, since we throw
            agentDto.PropertiesCount = await _propertyRepository.GetNumberOfPropertiesOfAgent(agentId);

            return agentDto;
        }
    }
}
