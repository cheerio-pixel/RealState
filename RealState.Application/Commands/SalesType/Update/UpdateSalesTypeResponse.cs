namespace RealState.Application.Commands.SsalesType.Update
{
    public class UpdateSsalesTypeResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}