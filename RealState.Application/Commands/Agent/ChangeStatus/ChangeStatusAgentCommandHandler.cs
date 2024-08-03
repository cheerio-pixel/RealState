﻿using System.Net;

using MediatR;

using RealState.Application.Exceptions;
using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;

namespace RealState.Application.Commands.Agent.ChangeStatus
{
    public class ChangeStatusAgentCommandHandler
        : IRequestHandler<ChangeStatusAgentCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public ChangeStatusAgentCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ChangeStatusAgentCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.AgentId);
            if (user is null)
                HttpStatusCode
                    .NoContent
                    .Because("There isn`t any user with that id")
                    .Throw();

            var result = await _userRepository.ChangeStatus(request.AgentId, request.Status);
            if(!result)
                HttpStatusCode
                    .NoContent
                    .Because("There is a problem while update status of agent")
                    .Throw();

            return Unit.Value;
        }
    }
}
