using MediatR;

using RealState.Application.DTOs.PropertyType;

namespace RealState.Application.Queries.PropertyType.GetById
{
    public class GetByIdPropertyTypeQuery
    : IRequest<PropertyTypeDTO>
    {
        public Guid Id { get; set; }
    }
}