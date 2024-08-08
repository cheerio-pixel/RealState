using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.QueryFilters.User;
using RealState.MVC.ActionFilter;
using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers
{
    public class HomeController(IPropertyService propertyService,
                                IPropertyTypeService propertyTypeService,
                                IUserServices userServices,
                                IFavoriteService favoriteService) : Controller
    {
        private readonly IPropertyService _propertyService = propertyService;
        private readonly IPropertyTypeService _propertyTypeService = propertyTypeService;
        private readonly IUserServices _userServices = userServices;
        private readonly IFavoriteService _favoriteService = favoriteService;

        [HomeGuard]
        public async Task<IActionResult> Index(PropertyQueryFilter? filter)
        {
            if (User.IsLoggedIn() && User.GetMainRole() == RoleTypes.Client)
            {
                ViewBag.role = RoleTypes.Client;
                ViewBag.Favorites = await _favoriteService.GetAllFavoriteByUserId(User.GetId());
            }
            PropertyQueryFilter propertyQueryFilter = filter ?? new();
            var result = await _propertyService.ListPropertiesQueryable(propertyQueryFilter!);
            ViewBag.PropertysTypes = await _propertyTypeService.GetAllViewModel();
            ViewBag.Properties = result.Value;
            ViewBag.Filter = propertyQueryFilter;
            return View();
        }

        [HomeGuard]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            return await _propertyService.GetPropertyDetailsById(id).Match(
                success: s =>
                {
                    ViewBag.Property = s;
                    return View();
                },
                failure: _ => this.RedirectBack()
            );
        }

        [HomeGuard]
        public IActionResult Agents(UserQueryFilter filter)
        {
            filter ??= new UserQueryFilter();
            filter.Role = RoleTypes.StateAgent;

            ViewBag.Agents = _userServices.GetAll(filter).Value;
            return View();
        }

        [HttpGet("home/agent/{id}")]
        [HomeGuard]
        public async Task<IActionResult> Properties(Guid id)
        {
            var result = await _propertyService.GetPropertyByAgentIdWithInclude(id);
            ViewBag.Properties = result.Value;
            return View();
        }
    }
}
