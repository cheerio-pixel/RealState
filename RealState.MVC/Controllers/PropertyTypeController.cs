
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters.PropertyType;
using RealState.Application.ViewModel.PropertyType;
using RealState.MVC.Helpers;
using RealState.MVC.Models;

namespace RealState.MVC.Controllers
{
    public class PropertyTypeController
    : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;

        public PropertyTypeController(IPropertyTypeService propertyTypeService)
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

        public async Task<IActionResult> CreateEdit(Guid? id)
        {
            PropertyTypeSaveViewModel? model = null;
            if (id is { } i)
            {
                model = await _propertyTypeService.GetByIdSaveViewModel(i);
            }
            model ??= new();
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateEdit(PropertyTypeSaveViewModel model)
        {
            if (ModelState.IsValid)
            {
                Result<Unit> result;
                if (model.Id is null || model.Id == Guid.Empty)
                {
                    result = await _propertyTypeService.Add(model).Map(_ => Unit.T);
                }
                else
                {
                    result = await _propertyTypeService.Update(model, model.Id.Value);
                }
                if (result.IsFailure)
                {
                    ModelState.AggregateErrors(result.Errors);
                }
                return result.Match<IActionResult>(
                    success: u => RedirectToAction("Index", "PropertyType"),
                    failure: s => View(model)
                );
            }
            return RedirectToAction("CreateEdit", "PropertyType", new
            {
                id = model.Id
            });
        }

        public virtual async Task<IActionResult> Delete(Guid? id)
        {
            if (id is { } i)
            {
                await _propertyTypeService.Delete(i);
            }
            return this.RedirectBack();
        }
    }
}