using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Enums;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Favorite;
using RealState.MVC.Helpers;


namespace RealState.MVC.Controllers
{
    [Authorize(Roles = nameof(RoleTypes.Client))]
    public class FavoriteController(IFavoriteService favoriteService) : Controller
    {
        private readonly IFavoriteService _favoriteService = favoriteService;


        public async Task<IActionResult> MyProperties()
        {
            var userId = User.GetId();
            ViewBag.Favorites = await _favoriteService.FavoriteViewModels(userId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(Guid propertyId)
        {
            var userId = User.GetId();
            var favorite = new FavoriteSaveViewModel
            {
                PropertyId = propertyId,
                UserId = userId
            };
            await _favoriteService.Add(favorite);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(Guid favoriteId, string returnUrl = "")
        {
            await _favoriteService.Delete(favoriteId);
            return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction("Index", "Home");
        }
    }
}
