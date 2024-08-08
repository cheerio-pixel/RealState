using MediatR;

using RealState.Application.DTOs.User;

using Swashbuckle.AspNetCore.Annotations;


namespace RealState.Application.Queries.Agent.GetAll
{
    public class GetAllAgentQuery : IRequest<List<AgentDTO>>
    {
        /// <summary>
        /// The first name of the agent
        /// </summary>
        [SwaggerParameter(Description = "The first name of the agent")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name of the agent
        /// </summary>
        [SwaggerParameter(Description = "The last name of the agent")]
        public string? LastName { get; set; }

        /// <summary>
        /// The username of the agent
        /// </summary>
        [SwaggerParameter(Description = "The username of the agent")]
        public string? UserName { get; set; }

        /// <summary>
        /// The email of the agent
        /// </summary>
        [SwaggerParameter(Description = "The email of the agent")]
        public string? Email { get; set; }

        /// <summary>
        /// The identifier card of the agent
        /// </summary>
        [SwaggerParameter(Description = "The identifier card of the agent")]
        public string? IdentifierCard { get; set; }

        /// <summary>
        /// The phone number of the agent
        /// </summary>
        [SwaggerParameter(Description = "The phone number of the agent")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// The active status of the agent
        /// </summary>
        [SwaggerParameter(Description = "The active status of the agent")]
        public bool? Active { get; set; }
    }
}
