
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.PropertyType;
using RealState.Domain.Entities;
using RealState.MVC.Common;
using RealState.MVC.Models;

namespace RealState.MVC.Controllers
{
    public class PropertyTypeController
    : CrudController<PropertyTypeSaveViewModel, PropertyTypeViewModel, PropertyTypes, Guid, IPropertyTypeService>
    {
        private readonly IPropertyTypeService _propertyTypeService;

        public PropertyTypeController(IPropertyTypeService propertyTypeService,
                                      string controllerName = "PropertyType",
                                      string managerEndpoint = "Index")
        : base(propertyTypeService, controllerName, managerEndpoint)
        {
            _propertyTypeService = propertyTypeService;
        }

        public async Task<IActionResult> Index(PropertyTypeQueryFilter? filter)
        {
            PropertyTypeQueryFilter propertyTypeQueryFilter = filter ?? new();
            List<PropertyTypeListItemViewModel> propertyTypeListItemViewModels
                = await _propertyTypeService.SearchPropertyType(propertyTypeQueryFilter);
            return View(new PropertyTypeIndexViewModel()
            {
                Filters = propertyTypeQueryFilter,
                Result = propertyTypeListItemViewModels
            });
        }

        [HttpGet]
        public override Task<IActionResult> Delete(Guid? id)
        {
            return base.Delete(id);
        }

        [HttpGet]
        public override Task<IActionResult> CreateEdit(Guid? id)
        {
            return base.CreateEdit(id);
        }

        [HttpPost]
        public override Task<IActionResult> CreateEdit(PropertyTypeSaveViewModel model)
        {
            return base.CreateEdit(model);
        }
    }
}