using RealState.Application.DTOs.User;
using RealState.Application.ViewModel.Pictures;
using RealState.Application.ViewModel.PropertyType;
using RealState.Application.ViewModel.SalesType;
using RealState.Application.ViewModel.Upgrades;
using RealState.Application.ViewModel.User;

namespace RealState.Application.ViewModel.Property
{
    public class PropertyDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string Code { get; set; } = null!;
        public int Meters { get; set; }
        public List<PicturesViewModel> Pictures { get; set; } = [];
        public PropertyTypeViewModel PropertyTypes { get; set; } = null!;
        public SalesTypeViewModel SalesTypes { get; set; } = null!;
        public AgentSaveViewModel Agent { get; set; } = null!;
        public ApplicationUserDTO ApplicationUser { get; set; } = null!;
        public List<UpgradesViewModel> Upgrade { get; set; } = [];
    }
}
