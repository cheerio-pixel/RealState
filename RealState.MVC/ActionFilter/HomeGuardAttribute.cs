
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using RealState.Application.Enums;
using RealState.MVC.Helpers;

namespace RealState.MVC.ActionFilter
{
    internal class HomeGuardAttribute
    : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is Controller controller
                && controller.User.IsLoggedIn()
                && controller.User.GetMainRole() != RoleTypes.Client)
            {
                context.Result = new RedirectToActionResult("ChooseRole", "Account", new object());
            }
            else
            {
                await next();
            }
        }
    }
}