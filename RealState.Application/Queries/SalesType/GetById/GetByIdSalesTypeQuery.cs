using MediatR;

using RealState.Application.DTOs.SalesType;

using Swashbuckle.AspNetCore.Annotations;

namespace RealState.Application.Queries.SalesType.GetById
{
    public class GetByIdSalesTypeQuery
    : IRequest<SalesTypeDTO>
    {
        [SwaggerParameter(Description = "Id of the Sales Type to query")]
        public Guid Id { get; set; }
    }
}