using Microsoft.AspNetCore.Mvc;

using RealState.Application.Interfaces.Services;
namespace RealState.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService _propertyService;

        public HomeController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _propertyService.GetAllWithIncludes();
         
            ViewBag.Properties = result.Value;
            return View();
        }

        public async  Task<IActionResult> Details()
        {
            var test = await _propertyService.GetPropertyDetailsById(Guid.Parse("588BA8F9-614B-4E67-2C61-08DCB3167A35"));
            ViewBag.Property = test.Value;
            return View();
        }
    }
}
