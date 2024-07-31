using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
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
            ViewData["AdminRole"] = await _roleServices.GetByNameAsync(RoleTypes.Admin.ToString());
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            //viewModel.Picture = PictureHelper.GetAdminPicture();

            var result = await _accountServices.RegisterAsync(viewModel);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(viewModel);
            }
            return View();
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

            var result = await _userServices.UpdateAsync(viewModel.Id, viewModel);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(viewModel);
            }
            return View();
        }
    }
}
