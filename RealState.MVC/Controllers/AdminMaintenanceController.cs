﻿using Microsoft.AspNetCore.Mvc;

using RealState.Application.Helper;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.User;
using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers
{
    public class AdminMaintenanceController(IUserServices userServices, IRoleServices roleServices, IAccountServices accountServices) : Controller
    {
        private readonly IUserServices _userServices = userServices;
        private readonly IRoleServices _roleServices = roleServices;
        private readonly IAccountServices _accountServices = accountServices;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            //ViewData["AdminRole"] = _roleServices.Get(RoleTypes.Admin.ToString());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserSaveViewModel viewModel)
        {
            //ViewData["AdminRole"] = _roleServices.Get(RoleTypes.Admin.ToString());
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
    }
}
