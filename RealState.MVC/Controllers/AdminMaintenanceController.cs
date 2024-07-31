using System.Security.Claims;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.User;
using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers
{
    public class AdminMaintenanceController(IUserServices userServices, IRoleServices roleServices, IAccountServices accountServices, IMapper mapper) : Controller
    {
        private readonly IUserServices _userServices = userServices;
        private readonly IRoleServices _roleServices = roleServices;
        private readonly IAccountServices _accountServices = accountServices;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var role = await _roleServices.GetByNameAsync(RoleTypes.Admin.ToString());
            ViewData["AdminRole"] = role.Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserSaveViewModel viewModel)
        {
            var role = await _roleServices.GetByNameAsync(RoleTypes.Admin.ToString());
            ViewData["AdminRole"] = role.Value;

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
            return View(nameof(Index));
        }

        public async Task<IActionResult> Update(string Id)
        {
            var result = await _userServices.GetByIdAsync(Id);
            var user = result.Value;
            var vw = _mapper.Map<UserSaveViewModel>(user);
            return View(vw);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserSaveViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _userServices.UpdateAsync(viewModel.Id, currentUserId, viewModel);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(viewModel);
            }
            return View(nameof(Index));
        }
    }
}
