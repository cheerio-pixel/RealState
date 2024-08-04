using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters.User;
using RealState.Application.ViewModel.User;
using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers
{
    [Authorize(Roles = nameof(RoleTypes.Admin))]
    public class AgentMaintenanceController(IUserServices userServices, IRoleServices roleServices, IAccountServices accountServices, IMapper mapper) : Controller
    {
        private readonly IUserServices _userServices = userServices;
        private readonly IRoleServices _roleServices = roleServices;
        private readonly IAccountServices _accountServices = accountServices;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
            var result = _userServices.GetAll(new UserQueryFilter() { Role = RoleTypes.StateAgent });
            ViewData["Agents"] = result.Value;
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var role = await _roleServices.GetByNameAsync(nameof(RoleTypes.StateAgent));
            ViewData["AgentRole"] = role.Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserSaveViewModel viewModel)
        {
            var role = await _roleServices.GetByNameAsync(nameof(RoleTypes.StateAgent));
            ViewData["AgentRole"] = role.Value;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            viewModel.Picture = PictureHelper.GetDefaultUserImage();

            var result = await _accountServices.RegisterAsync(viewModel);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var result = await _userServices.GetByIdAsync(id);

            if (result.Value == null) return View(nameof(Index));
            var user = result.Value;
            var vw = _mapper.Map<UserSaveViewModel>(user);
            return View(vw);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserSaveViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userServices.UpdateAsync(viewModel.Id!, currentUserId!, viewModel);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeStatus(string userId, bool status)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userServices.ChangeStatusAsync(userId, currentUserId!, status);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _userServices.DeleteAsync(userId);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
