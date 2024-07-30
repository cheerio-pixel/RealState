namespace RealState.Application.ViewModel.Pictures
{
    public class PicturesSaveViewModel
    {
        public Guid Id { get; set; }
        public string Picture { get; set; } = null!;
        public Guid PropertyId { get; set; }
    }
}
