using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using RealState.Application.Extras;

namespace RealState.Application.ViewModel.Property
{
    public class PropertSaveViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Price is required")]
        [RangeD("0", "79228162514264337593543950335", ErrorMessage = "Must be greater than 0")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Rooms is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Must equal or greater than 1")]
        public int Rooms { get; set; }
        [Required(ErrorMessage = "Bathrooms is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Must equal or greater than 1")]
        public int Bathrooms { get; set; }
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Meters is required")]
        [RangeD("0", "79228162514264337593543950335", ErrorMessage = "Must be greater than 0")]
        public decimal Meters { get; set; }
        public List<IFormFile> Pictures { get; set; } = null!;
        [Required(ErrorMessage = "PropertyTypeId is required")]
        public Guid PropertyTypeId { get; set; }
        [Required(ErrorMessage = "SalesTypeId is required")]
        public Guid SalesTypeId { get; set; }
        [Required(ErrorMessage = "UpgradeId is required")]
        public List<Guid> UpgradeId { get; set; } = [];

        public Guid AgentId { get; set; }
        public List<string> PicturesUrl { get; set; } = [];
    }
}
