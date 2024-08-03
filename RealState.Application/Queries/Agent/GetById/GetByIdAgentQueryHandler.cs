using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.User;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;

namespace RealState.Application.Queries.Agent.GetById
{
    public class GetByIdAgentQueryHandler
        : IRequestHandler<GetByIdAgentQuery, AgentDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetByIdAgentQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<AgentDTO> Handle(GetByIdAgentQuery request, CancellationToken cancellationToken)
        {
            var agent = await _userRepository.Get(request.AgentId);
            if(agent is null)
                HttpStatusCode
                .NoContent
                .Because("There is no content")
                .Throw();

            var agentDto = _mapper.Map<AgentDTO>(agent);

            var count = 0; // _propertyRepository.GetPropertiesCountByAgentId(Guid.Parse(agentDto.Id));
            agentDto.PropertiesCount = count;

            return agentDto;
        }
    }
}
