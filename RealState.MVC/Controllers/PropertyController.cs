using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using RealState.Application.Commands.Property.Create;
using RealState.Application.Dtos;
using RealState.Application.ViewModel.Property;

namespace RealState.MVC.Controllers
{

    public class PropertyController(IMediator mediator, IMapper mapper) : Controller
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyViewModel vm)
        {

            var command = _mapper.Map<CreatePropertyCommand>(vm);
         //   TResult<PropertyViewModel> result = await _mediator.Send(command);
         var result = new CreatePropertyCommandValidator().Validate(command);

            if (result.IsValid)
            {
                return RedirectToAction("Index");
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View();
            
        }
    }
}
