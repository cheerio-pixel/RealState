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
        /// Filter by the price of the property.
        /// </summary>
        [SwaggerParameter(Description = "Filter by the price of the property")]
        public decimal Price { get; set; }

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
