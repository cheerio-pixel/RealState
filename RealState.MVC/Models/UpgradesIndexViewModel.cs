using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.Upgrades;

namespace RealState.MVC.Models
{
    public class UpgradesIndexViewModel
    {
        public required UpgradesQueryFilter Filters { get; set; }
        public required List<UpgradesViewModel> Result { get; set; }
    }
}