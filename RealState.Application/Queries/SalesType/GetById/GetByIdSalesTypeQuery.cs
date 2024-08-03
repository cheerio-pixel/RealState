using MediatR;

using RealState.Application.DTOs.SalesType;

namespace RealState.Application.Queries.SalesType.GetById
{
    public class GetByIdSalesTypeQuery
    : IRequest<SalesTypeDTO>
    {
        public Guid Id { get; set; }
    }
}