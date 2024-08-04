namespace RealState.Application.Commands.Upgrade.Update
{
    public class UpdateUpgradeResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}