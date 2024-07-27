using Microsoft.AspNetCore.Mvc;

using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.SalesType;
using RealState.Domain.Entities;
using RealState.MVC.Common;
using RealState.MVC.Models;

namespace RealState.MVC.Controllers
{
    public class SalesTypeController
    : CrudController<SalesTypeSaveViewModel, SalesTypeViewModel, SalesTypes, Guid, ISalesTypesService>
    {
        private readonly ISalesTypesService _salesTypesService;

        public SalesTypeController(ISalesTypesService salesTypesService)
        : base(salesTypesService, "SalesType", "Index")
        {
            _salesTypesService = salesTypesService;
        }

        public async Task<IActionResult> Index(SalesTypesQueryFilter? filter)
        {
            SalesTypesQueryFilter salesTypesQueryFilter = filter ?? new();
            List<SalesTypeListItemViewModel> salesTypeListItemViewModels
                = await _salesTypesService.SearchSalesTypes(salesTypesQueryFilter);
            return View(new SalesTypeIndexViewModel()
            {
                Filters = salesTypesQueryFilter,
                Result = salesTypeListItemViewModels
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
        public override Task<IActionResult> CreateEdit(SalesTypeSaveViewModel model)
        {
            return base.CreateEdit(model);
        }
    }
}