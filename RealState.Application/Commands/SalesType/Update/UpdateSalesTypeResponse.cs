namespace RealState.Application.Commands.SalesType.Update
{
    public class UpdateSalesTypeResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}