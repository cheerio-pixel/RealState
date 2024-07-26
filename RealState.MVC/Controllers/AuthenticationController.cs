using Microsoft.AspNetCore.Mvc;

using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Account;
using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAccountServices _accountServices;
        public AuthenticationController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
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
            => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel user)
        {
            if (!ModelState.IsValid) return View(user);

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
