using MediatR;
using RealState.Application.DTOs.Property;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.Property.GetAll
{
    public class GetAllPropertyQuery : IRequest<List<PropertyDTO>>
    {
        /// <summary>
        /// Filter by the property code.
        /// </summary>
        [SwaggerParameter(Description = "Filter by the property code")]
        public string? Code { get; set; }

        /// <summary>
        /// Filter by the number of rooms.
        /// </summary>
        [SwaggerParameter(Description = "Filter by the number of rooms")]
        public int Rooms { get; set; }

        /// <summary>
        /// Filter by the number of bathrooms.
        /// </summary>
        [SwaggerParameter(Description = "Filter by the number of bathrooms")]
        public int Bathrooms { get; set; }

        /// <summary>
        /// Filter by the mininum price to have
        /// </summary>
        [SwaggerParameter(Description = "Filter by the mininum price to have")]
        public decimal MinPrice { get; set; }

        /// <summary>
        /// Filter by the maximum price to have
        /// </summary>
        [SwaggerParameter(Description = "Filter by the maximum price to have")]
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Filter by the property type ID.
        /// </summary>
        [SwaggerParameter(Description = "Filter by the property type ID")]
        public Guid? PropertyTypeId { get; set; } = null;

        /// <summary>
        /// Filter by the agent ID.
        /// </summary>
        [SwaggerParameter(Description = "Filter by the owner agent")]
        public Guid? AgentId { get; set; }
    }
}
