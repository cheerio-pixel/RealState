namespace RealState.Application.ViewModel.PropertyType
{
    public class PropertyTypeListItemViewModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int NumberOfProperties { get; set; }
    }
}