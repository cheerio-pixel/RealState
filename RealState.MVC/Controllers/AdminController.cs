using Microsoft.AspNetCore.Mvc;

using RealState.Application.Interfaces.Services;

namespace RealState.MVC.Controllers
{
    public class AdminController(IUserServices userServices) : Controller
    {
        private readonly IUserServices _userServices = userServices;

        public async Task<IActionResult> Index()
        {
            ViewBag.Statistics = await _userServices.UserStatisticsDtoAsync();
            return View();
        }
    }
}
