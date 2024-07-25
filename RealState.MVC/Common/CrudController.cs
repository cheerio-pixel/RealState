using Microsoft.AspNetCore.Mvc;

using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel;
using RealState.Domain.Common;
using RealState.MVC.Helpers;

namespace RealState.MVC.Common
{
    // Only works with id types that are structs
    public abstract class CrudController
    <TSaveViewModel, TViewModel, TEntity, TKey, TService>
    : Controller
    where TKey : struct
    where TEntity : Entity<TKey>, new()
    where TSaveViewModel : BaseSaveViewModel<TKey?>, new()
    where TViewModel : class
    where TService : IGenericService<TSaveViewModel, TViewModel, TEntity, TKey>
    {
        private readonly TService _service;
        private readonly string _managerEndpoint;
        private readonly string _controllerName;

        protected CrudController(TService service,
                              string controllerName,
                              string managerEndpoint = "Index")
        {
            _service = service;
            _managerEndpoint = managerEndpoint;
            _controllerName = controllerName;
        }

        public virtual async Task<IActionResult> CreateEdit(TKey? id)
        {
            TSaveViewModel? model = null;
            if (id is { } i)
            {
                model = await _service.GetByIdSaveViewModel(i);
            }
            model ??= new();
            return View(model);
        }

        public virtual async Task<IActionResult> CreateEdit(TSaveViewModel model)
        {
            if (ModelState.IsValid)
            {
                Result<Unit> result;
                if (model.Id is { } id)
                {
                    result = await _service.Update(model, id);
                }
                else
                {
                    result = await _service.Add(model).Map(_ => Unit.T);
                }
                if (result.IsFailure)
                {
                    ModelState.AggregateErrors(result.Errors);
                }
                return result.Match<IActionResult>(
                    success: u => RedirectToAction(_managerEndpoint, _controllerName),
                    failure: s => View(model)
                );
            }
            return View(model);
        }

        public virtual async Task<IActionResult> Delete(TKey? id)
        {
            if (id is { } i)
            {
                await _service.Delete(i);
            }
            return this.RedirectBack();
        }
    }
}