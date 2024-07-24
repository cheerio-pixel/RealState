using RealState.Application.QueryFilters.PropertyType;
using RealState.Application.ViewModel.PropertyType;

namespace RealState.MVC.Models
{
    public class PropertyTypeIndexViewModel
    {
        public required PropertyTypeQueryFilter Filters { get; set; }
        public required List<PropertyTypeListItemViewModel> Result { get; set; }
    }
}