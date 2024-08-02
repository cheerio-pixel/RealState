using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.SalesType;

namespace RealState.MVC.Models
{
    public class SalesTypeIndexViewModel
    {
        public required SalesTypesQueryFilter Filters { get; set; }
        public required List<SalesTypeListItemViewModel> Result { get; set; }
    }
}