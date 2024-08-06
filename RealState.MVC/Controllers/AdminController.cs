using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
using RealState.Application.Interfaces.Services;

namespace RealState.MVC.Controllers
{
    [Authorize(Roles = nameof(RoleTypes.Admin))]
    public class AdminController(IUserServices userServices, IPropertyService propertyService) : Controller
    {
        private readonly IUserServices _userServices = userServices;
        private readonly IPropertyService _propertyService = propertyService;

        public async Task<IActionResult> Index()
        {
            ViewBag.Statistics = await _userServices.UserStatisticsDtoAsync();
            ViewBag.PropertyCount = await _propertyService.GetPropertyCount();
            return View();
        }
    }
}
