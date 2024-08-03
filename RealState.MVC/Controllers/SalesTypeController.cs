
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.SalesType;
using RealState.Domain.Entities;
using RealState.MVC.Common;
using RealState.MVC.Models;

namespace RealState.MVC.Controllers
{
    [Authorize(Roles = nameof(RoleTypes.Admin))]
    public class SalesTypeController(ISalesTypesService salesTypesService)
        : CrudController<SalesTypeSaveViewModel, SalesTypeViewModel, SalesTypes, Guid, ISalesTypesService>(salesTypesService, "SalesType", "Index")
    {
        private readonly ISalesTypesService _salesTypesService = salesTypesService;

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