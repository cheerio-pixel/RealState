using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Account;
using RealState.Application.ViewModel.User;
using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers
{
    public class AccountController(IAccountServices accountServices, IRoleServices roleServices, ILogger<AccountController> logger) : Controller
    {
        private readonly IAccountServices _accountServices = accountServices;
        private readonly IRoleServices _roleServices = roleServices;
        private readonly ILogger<AccountController> _logger = logger;

        public IActionResult SignIn(string? returnUrl)
            => View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });

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
            if (login.ReturnUrl is not null)
            {
                return LocalRedirect(login.ReturnUrl);
            }
            return RedirectToAction(nameof(ChooseRole));
            // return RedirectToAction("Index", "Home");
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

            var picture = PictureHelper.UploadFile(user.File!, user.UserName, "Users");
            user.Picture = picture;
            var result = await _accountServices.RegisterAsync(user);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(user);
            }

            return View(nameof(SignIn));
        }

        public async Task<IActionResult> ConfirmAccount(ConfirmAccountViewModel confimAccountVw)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Success"] = false;
                return View(confimAccountVw);
            }

            var result = await _accountServices.ConfirmAccountAsync(confimAccountVw);
            if (result.IsFailure)
            {
                ViewData["Success"] = false;
                ModelState.AggregateErrors(result.Errors);
                return View(confimAccountVw);
            }

            ViewData["Success"] = true;
            return View();
        }

        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordVm)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordVm);
            }

            var result = await _accountServices.ForgotPasswordAsync(forgotPasswordVm);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(forgotPasswordVm);
            }
            return View(nameof(SignIn));
        }

        public IActionResult ResetPassword(string Token, string UserId) => View(new ResetPasswordViewModel() { Token = Token, UserId = UserId });

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel ResetPasswordVm)
        {
            if (!ModelState.IsValid)
            {
                return View(ResetPasswordVm);
            }

            var result = await _accountServices.ResetPasswordAsync(ResetPasswordVm);
            if (result.IsFailure)
            {
                ModelState.AggregateErrors(result.Errors);
                return View(ResetPasswordVm);
            }
            return View(nameof(SignIn));
        }

        public async Task<IActionResult> LogOut()
        {
            await _accountServices.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult ChooseRole()
        {
            string role = User.GetUnparsedMainRole();
            if (!Enum.TryParse(role, out RoleTypes roleT))
            {
                // Do not throw exception, log it
                _logger.LogError("Unknown role {Role} when coming from {PreviousUrl}",
                                   role,
                                   Request.Headers.Referer);
                return RedirectToAction("Index", "Home");
            }
            role = roleT switch
            {
                RoleTypes.Admin => "Admin",
                RoleTypes.Client => "home",
                RoleTypes.StateAgent => "Agent",
                _ => "Home"
            };

            return RedirectToAction("Index", role);
        }
    }
}
