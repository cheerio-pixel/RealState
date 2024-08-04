using RealState.Application.ViewModel.Property;

namespace RealState.Application.ViewModel.Favorite
{
    public class FavoriteViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PropertyId { get; set; }
        public PropertyViewModel? Property { get; set; }
    }
}
