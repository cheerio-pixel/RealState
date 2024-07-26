using Microsoft.AspNetCore.Mvc;
namespace RealState.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
