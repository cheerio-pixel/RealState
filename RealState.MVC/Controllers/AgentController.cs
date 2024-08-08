using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.Enums;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.Pictures;
using RealState.Application.ViewModel.PropertiesUpgrades;
using RealState.Application.ViewModel.Property;
using RealState.Application.ViewModel.User;
using RealState.MVC.ActionFilter;
using RealState.MVC.Helpers;

namespace RealState.MVC.Controllers;

[Authorize(Roles = nameof(RoleTypes.StateAgent))]
public class AgentController(IPropertyService propertyService
    , IMapper mapper,
    IPictureService pictureService,
    IPropertyUpgradeService propertyUpgradeService,
    IPropertyTypeService propertyTypeService,
    IUserServices userServices,
    ILogger<AgentController> logger) : Controller
{
    private readonly IPropertyService _propertyService = propertyService;
    private readonly IPictureService _pictureService = pictureService;
    private readonly IPropertyUpgradeService _propertyUpgradeService = propertyUpgradeService;
    private readonly IMapper _mapper = mapper;
    private readonly IUserServices _userServices = userServices;
    private readonly ILogger<AgentController> _logger = logger;
    private readonly IPropertyTypeService _propertyTypeService = propertyTypeService;

    public async Task<IActionResult> Index(PropertyQueryFilter? filter)
    {
        PropertyQueryFilter propertyQueryFilter = filter ?? new();
        propertyQueryFilter.AgentId = User.GetId();
        Result<List<PropertyViewModel>> result = await _propertyService.ListPropertiesQueryable(propertyQueryFilter);
        ViewBag.Properties = result.Value;
        ViewBag.PropertysTypes = await _propertyTypeService.GetAllViewModel();
        ViewBag.Filter = propertyQueryFilter;
        return View();
    }

    [ServiceFilter(typeof(SetAttributesViewBag))]
    public IActionResult Create()
    {
        return View(new PropertSaveViewModel());
    }

    public async Task<IActionResult> Profile()
    {
        var userId = User.GetId();
        ViewBag.Id = userId;

        var user = await _userServices.GetByIdAsync(userId.ToString());
        var userDto = _mapper.Map<AgentSaveViewModel>(user.Value);
        userDto.PictureUrl = user.Value!.Picture;
        return View(userDto);
    }

    [HttpGet("agent/Update/{id}")]
    [ServiceFilter(typeof(SetAttributesViewBag))]
    public async Task<IActionResult> Update(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return Redirect("/Agent/Index");
        }

        var property = await _propertyService.GetByIdSaveViewModel(Guid.Parse(id));
        return property is null ? Redirect("/Agent/Index") : View("Create", property);
    }

    [HttpPost]
    [ServiceFilter(typeof(SetAttributesViewBag))]
    public async Task<IActionResult> Create(PropertSaveViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        vm.AgentId = User.GetId();

        Result<PropertSaveViewModel> result = await _propertyService.Add(vm);
        if (result.IsFailure)
        {
            ModelState.AggregateErrors(result.Errors);
            return View(vm);
        }
        PropertSaveViewModel property = result.Value;

        List<PicturesSaveViewModel> pictures = vm.Pictures.ConvertAll(picture =>
        new PicturesSaveViewModel
        {
            Picture = PictureHelper.UploadFile(picture, property.Id.ToString(), "Properties"),
            PropertyId = property.Id
        });

        var pictureResult = await _pictureService.AddPictures(pictures);
        if (pictureResult.IsFailure)
        {
            foreach (var e in pictureResult.Errors)
            {
                ModelState.AddModelError(nameof(vm.Pictures), e.Message);
            }
            await _propertyService.Delete(property.Id);
            return View(vm);
        }

        PropertyUpgradeSaveViewModel propertyUpgrade = _mapper.Map<PropertyUpgradeSaveViewModel>(vm);
        propertyUpgrade.PropertyId = property.Id;
        Result<PropertyUpgradeSaveViewModel> propertyUpgradeResult = await _propertyUpgradeService.Add(propertyUpgrade);
        if (propertyUpgradeResult.IsFailure)
        {
            ModelState.AggregateErrors(propertyUpgradeResult.Errors);
            await _propertyService.Delete(property.Id);
            return View(vm);
        }

        return !pictureResult.IsSuccess ? View(vm) : LocalRedirect("/Agent/index");
    }

    [HttpPost]
    [ServiceFilter(typeof(SetAttributesViewBag))]
    public async Task<IActionResult> Update(PropertSaveViewModel vm)
    {
        ModelState.Remove(nameof(vm.Pictures));
        vm.AgentId = User.GetId();
        if (!ModelState.IsValid)
        {
            return View($"create", vm);
        }

        var result = await _propertyService.Update(vm, vm.Id);
        if (!result.IsSuccess)
        {
            return View("create", vm);
        }


        // Update propertyupgrades
        PropertyUpgradeSaveViewModel proupd = _mapper.Map<PropertyUpgradeSaveViewModel>(vm);
        proupd.PropertyId = vm.Id;
        await _propertyUpgradeService.UpdatePropertyUpgradesByPropertyId(proupd, vm.Id);

        //update pictures
        if (vm.Pictures != null)
        {
            if (vm.Pictures.Count > 4)
            {
                ModelState.AddModelError(nameof(vm.Pictures), "You can only upload 4 pictures");
                return View("create", vm);
            }
            List<PicturesSaveViewModel> pictures = [];
            foreach (var picture in vm.Pictures)
            {
                pictures.Add(new PicturesSaveViewModel
                {
                    Picture = PictureHelper.UploadFile(picture, vm.Id.ToString(), "Properties"),
                    PropertyId = vm.Id
                });
            }
            await _pictureService.UpdatePicturesByPropertyId(pictures, vm.Id);
        }

        return RedirectToAction("Index", "Agent");
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
            _logger.LogError(ex, "Error when trying to delete property {PropertyId}", id);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Profile(AgentSaveViewModel vm)
    {
        ModelState.Remove(nameof(vm.Picture));
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        if (vm.Picture != null)
        {
            vm.PictureUrl = PictureHelper.UploadFile(vm.Picture, vm.Id.ToString(), "Users");
        }

        var vmDto = _mapper.Map<UserSaveViewModel>(vm);
        vmDto.Picture = vm.PictureUrl!;
        var result = await _userServices.UpdateAgent(vm.Id.ToString(), vmDto);

        return !result.IsSuccess ? View(vm) : RedirectToAction("Index", "Agent");
    }
}
