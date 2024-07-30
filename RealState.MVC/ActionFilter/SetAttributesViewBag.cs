﻿using Microsoft.AspNetCore.Mvc.Filters;
using RealState.Application.Interfaces.Services;
namespace RealState.MVC.ActionFilter
{
    public class SetAttributesViewBag(IPropertyTypeService propertyTypeService, ISalesTypesService salesTypeService, IUpgradesService upgradesService) : IAsyncActionFilter
    {
        private readonly IPropertyTypeService _propertyTypeService = propertyTypeService;
        private readonly ISalesTypesService _salesTypeService = salesTypeService;
        private readonly IUpgradesService _upgradesService = upgradesService;

      
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as Microsoft.AspNetCore.Mvc.Controller;
            if (controller != null)
            {
                controller.ViewBag.PropertyTypes = await _propertyTypeService.GetAllViewModel();
                controller.ViewBag.SalesTypes = await _salesTypeService.GetAllViewModel();
                controller.ViewBag.Upgrades = await _upgradesService.GetAllViewModel();
            }
            await next();
        }
    }
}
