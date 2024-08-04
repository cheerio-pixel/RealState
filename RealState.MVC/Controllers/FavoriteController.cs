

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Favorite;
using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers
{
    public class FavoriteController(IFavoriteService favoriteService) : Controller
    {
        private readonly IFavoriteService _favoriteService = favoriteService;

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
        public async Task<IActionResult> RemoveFavorite(Guid favoriteId)
        {
            await _favoriteService.Delete(favoriteId);
            return RedirectToAction("Index", "Home");
        }
    }
}
