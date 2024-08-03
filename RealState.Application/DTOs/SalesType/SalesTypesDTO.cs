namespace RealState.Application.DTOs.SalesType
{
    public class SalesTypeDTO
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}