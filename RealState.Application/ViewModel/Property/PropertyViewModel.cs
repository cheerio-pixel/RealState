using Microsoft.AspNetCore.Http;

namespace RealState.Application.ViewModel.Property
{
    public class PropertyViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string Code { get; set; } = null!;
        public int Meters { get; set; }
        public List<IFormFile> Pictures { get; set; } = [];
        public Guid PropertyTypeId { get; set; }
        public Guid SalesTypeId { get; set; }
        public List<Guid> UpgradeId { get; set; } = [];
    }
}
