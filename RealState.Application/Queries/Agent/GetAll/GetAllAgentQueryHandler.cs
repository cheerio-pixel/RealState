using System.Net;

using AutoMapper;

using MediatR;

using RealState.Application.DTOs.User;
using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;

namespace RealState.Application.Queries.Agent.GetAll
{
    public class GetAllAgentQueryHandler
        : IRequestHandler<GetAllAgentQuery, List<AgentDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _propertyRepository;

        public GetAllAgentQueryHandler(IUserRepository userRepository, IMapper mapper, IPropertyRepository propertyRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }
        public async Task<List<AgentDTO>> Handle(GetAllAgentQuery request, CancellationToken cancellationToken)
        {
            var agents = _userRepository.Get(request.Filter).ToList();
            if(!agents.Any())
                HttpStatusCode
                .NoContent
                .Because("There is no content")
                .Throw();

            var agentDto = _mapper.Map<List<AgentDTO>>(agents);

            foreach (var item in agentDto)
            {
                var count = 0; // _propertyRepository.GetPropertiesCountByAgentId(Guid.Parse(item.Id));
                item.PropertiesCount = count;
            }

            return agentDto;
        }
    }
}
