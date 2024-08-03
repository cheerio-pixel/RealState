using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.Upgrades;
using RealState.Domain.Entities;
using RealState.MVC.Common;
using RealState.MVC.Models;

namespace RealState.MVC.Controllers
{
    [Authorize(Roles = nameof(RoleTypes.Admin))]
    public class UpgradeController
    : CrudController<UpgradesSaveViewModel, UpgradesViewModel, Upgrades, Guid, IUpgradesService>
    {
        private readonly IUpgradesService _upgradesService;

        public UpgradeController(IUpgradesService upgradesService)
        : base(upgradesService, "Upgrade", "Index")
        {
            _upgradesService = upgradesService;
        }

        public async Task<IActionResult> Index(UpgradesQueryFilter? filter)
        {
            UpgradesQueryFilter upgradesQueryFilter = filter ?? new();
            List<UpgradesViewModel> upgradesViewModels = await _upgradesService.SearchUpgrades(upgradesQueryFilter);
            return View(new UpgradesIndexViewModel()
            {
                Filters = upgradesQueryFilter,
                Result = upgradesViewModels
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
        public override Task<IActionResult> CreateEdit(UpgradesSaveViewModel model)
        {
            return base.CreateEdit(model);
        }
    }
}