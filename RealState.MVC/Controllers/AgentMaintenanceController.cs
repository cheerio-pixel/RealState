using AutoMapper;

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Enums;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters.User;

using RealState.Application.ViewModel.User;

using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers
{
     public class AgentMaintenanceController(IUserServices userServices, IRoleServices roleServices, IAccountServices accountServices, IMapper mapper) : Controller
     {
        private readonly IUserServices _userServices = userServices;
        private readonly IRoleServices _roleServices = roleServices;
        private readonly IAccountServices _accountServices = accountServices;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
            var result = _userServices.GetAll(new UserQueryFilter() { Role = RoleTypes.StateAgent });
            var agents = result.Value;
            ViewData["Agents"] = agents;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(string userId, bool status)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userServices.ChangeActiveStatusAsync(userId, currentUserId, status);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
            }
            return View(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _userServices.DeleteAsync(userId);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(nameof(Index));
            }
            return View(nameof(Index));
        }
    }
}
