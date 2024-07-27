using Microsoft.AspNetCore.Mvc;

using RealState.Application.Helper;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Account;
using RealState.Application.ViewModel.User;
using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly IRoleServices _roleServices;

        public AuthenticationController(IAccountServices accountServices, IRoleServices roleServices)
        {
            _accountServices = accountServices;
            _roleServices = roleServices;
        }

        public IActionResult SignIn()
            => View();

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            var result = await _accountServices.SignInAsync(login);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(login);
            }

            return View(nameof(Index));
        }

        public IActionResult Register()
        {
            ViewData["Roles"] = _roleServices.GetBasicRoles().Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel user)
        {
            ViewData["Roles"] = _roleServices.GetBasicRoles().Value;

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var picture = PictureHelper.UploadFile(user.File, user.UserName, "Users");
            user.Picture = picture;
            var result = await _accountServices.RegisterAsync(user);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(user);
            }

            return View(nameof(SignIn));
        }
    }
}
