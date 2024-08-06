using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.QueryFilters.User;
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
        public readonly IUserServices _userServices = userServices;
        public readonly IFavoriteService _favoriteService = favoriteService;

        public async Task<IActionResult> Index(PropertyQueryFilter? filter)
        {
            if (User.IsLoggedIn())
            {
                if (User.GetMainRole() == RoleTypes.Client)
                {
                    ViewBag.role = RoleTypes.Client;
                    ViewBag.Favorites = await _favoriteService.GetAllFavoriteByUserId(User.GetId());
                }
                else
                {
                    return RedirectToAction("ChooseRole", "Account");
                }

            }
            PropertyQueryFilter propertyQueryFilter = filter ?? new();
            var result = await _propertyService.ListPropertiesQueryable(propertyQueryFilter!);
            ViewBag.PropertysTypes = await _propertyTypeService.GetAllViewModel();
            ViewBag.Properties = result.Value;
            return View();
        }

        public async Task<IActionResult> Details()
        {
            var test = await _propertyService.GetPropertyDetailsById(Guid.Parse("588BA8F9-614B-4E67-2C61-08DCB3167A35"));
            ViewBag.Property = test.Value;
            return View();
        }

        public IActionResult Agents(UserQueryFilter filter)
        {
            filter ??= new UserQueryFilter();
            filter.Role = RoleTypes.StateAgent;

            ViewBag.Agents = _userServices.GetAll(filter).Value;
            return View();
        }

        [HttpGet("home/agent/{id}")]
        public async Task<IActionResult> Properties(Guid id)
        {

            var result = await _propertyService.GetPropertyByAgentIdWithInclude(id);
            ViewBag.Properties = result.Value;
            return View();
        }
    }
}
