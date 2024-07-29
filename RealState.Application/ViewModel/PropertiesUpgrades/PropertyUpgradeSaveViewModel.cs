namespace RealState.Application.ViewModel.PropertiesUpgrades
{
    public class PropertyUpgradeSaveViewModel
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public List<Guid>? UpgradeId { get; set; } = [];
    }
}
