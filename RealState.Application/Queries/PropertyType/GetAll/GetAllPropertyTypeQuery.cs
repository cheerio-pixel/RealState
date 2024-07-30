using RealState.Application.QueryFilters.PropertyType;

namespace RealState.Application.Queries.PropertyType.GetAll
{
    public class GetAllPropertyTypeQuery
    {
        public required PropertyTypeQueryFilter Filters { get; set; }
    }
}