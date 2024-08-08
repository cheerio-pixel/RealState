namespace RealState.Application.QueryFilters
{
    public class PropertyQueryFilter
    {
        public string? Code { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal Price { get; set; }
        public Guid? PropertyTypeId { get; set; }
        public Guid? AgentId { get; set; }
    }
}
