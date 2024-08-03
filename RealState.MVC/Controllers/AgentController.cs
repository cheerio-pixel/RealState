using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealState.Application.DTOs.User;
using RealState.Application.Enums;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Services;
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
    IUserServices userServices,
    ILogger<AgentController> logger) : Controller
{
    private readonly IPropertyService _propertyService = propertyService;
    private readonly IPictureService _pictureService = pictureService;
    private readonly IPropertyUpgradeService _propertyUpgradeService = propertyUpgradeService;
    private readonly IMapper _mapper = mapper;
    private readonly IUserServices _userServices = userServices;
    private readonly ILogger<AgentController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        Guid userId = User.GetId();
        var result = await _propertyService.GetPropertyByAgentId(userId);
        ViewBag.Properties = result.Value;
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

        ViewBag.Id = Guid.Parse(id);
        var property = await _propertyService.GetByIdSaveViewModel(Guid.Parse(id));
        if (property is null)
        {
            return Redirect("/Agent/Index");
        }

        return View("create", property);
    }

    [HttpPost]
    [ServiceFilter(typeof(SetAttributesViewBag))]
    public async Task<IActionResult> Create(PropertSaveViewModel vm)
    {
        List<PicturesSaveViewModel> pictures = [];
        vm.AgentId = User.GetId();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        PropertyUpgradeSaveViewModel proupd = _mapper.Map<PropertyUpgradeSaveViewModel>(vm);

        Result<PropertSaveViewModel> result = await _propertyService.Add(vm);
        if (!result.IsSuccess)
        {
            return View(vm);
        }



        foreach (var picture in vm.Pictures)
        {
            pictures.Add(new PicturesSaveViewModel
            {
                Picture = PictureHelper.UploadFile(picture, result.Value.Id.ToString(), "Properties"),
                PropertyId = result.Value.Id
            });
        }
        proupd.PropertyId = result.Value.Id;
        await _propertyUpgradeService.Add(proupd);
        var pictureResult = await _pictureService.AddPictures(pictures);

        return !pictureResult.IsSuccess ? View(vm) : RedirectToAction("index", "AgentController");
    }

    [HttpPost]
    [ServiceFilter(typeof(SetAttributesViewBag))]
    public async Task<IActionResult> Update(PropertSaveViewModel vm)
    {

        vm.AgentId = User.GetId();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        var result = await _propertyService.Update(vm, vm.Id);
        if (!result.IsSuccess)
        {
            return View(vm);
        }


        // Update propertyupgrades
        PropertyUpgradeSaveViewModel proupd = _mapper.Map<PropertyUpgradeSaveViewModel>(vm);
        proupd.PropertyId = vm.Id;
        await _propertyUpgradeService.UpdatePropertyUpgradesByPropertyId(proupd, vm.Id);

        //update pictures
        if (vm.Pictures != null && vm.Pictures.Count > 0)
        {
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

        return !result.IsSuccess ? View(vm) : Redirect("/Agent/Index");
    }
}
