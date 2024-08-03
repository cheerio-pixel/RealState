namespace RealState.Application.DTOs.Upgrade
{
    public class UpgradeDTO
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}