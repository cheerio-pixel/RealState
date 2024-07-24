namespace RealState.Application.DTOs.PropertyType
{
    public class PropertyTypeListItemDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int NumberOfProperties { get; set; }
    }
}
