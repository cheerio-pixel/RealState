using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Property;

namespace RealState.MVC.Controllers
{

    public class PropertyController(IMediator mediator, IMapper mapper, IPropertyService propertyService) : Controller
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPropertyService _propertyService = propertyService;
        private readonly IMapper _mapper = mapper;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            ViewBag.Id = id;
            var property = await _propertyService.GetByIdSaveViewModel(id);
            return View("create", property);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertSaveViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _propertyService.Add(vm);

            return !result.IsSuccess ? View(vm) : RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(PropertSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _propertyService.Update(vm, vm.Id);

            return !result.IsSuccess ? View(vm) : RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _propertyService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }


        }
    }
}
