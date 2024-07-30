using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Helper;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Pictures;
using RealState.Application.ViewModel.PropertiesUpgrades;
using RealState.Application.ViewModel.Property;
using RealState.MVC.ActionFilter;



namespace RealState.MVC.Controllers;

public class AgentController(IMediator mediator, IPropertyService propertyService
    , IMapper mapper,
    IUpgradesService upgradesService,
    ISalesTypesService salesTypesService,
    IPropertyTypeService propertyTypeService,
    IPictureService pictureService,
    IPropertyUpgradeService propertyUpgradeService) : Controller
{
    private readonly IMediator _mediator = mediator;
    private readonly IPropertyService _propertyService = propertyService;
    private readonly IUpgradesService _upgradesService = upgradesService;
    private readonly IPropertyTypeService _propertyTypeService = propertyTypeService;
    private readonly ISalesTypesService _salesTypeService = salesTypesService;
    private readonly IPictureService _pictureService = pictureService;
    private readonly IPropertyUpgradeService _propertyUpgradeService = propertyUpgradeService;
    private readonly IMapper _mapper = mapper;

    public IActionResult Index()
    {
        return View();
    }

    [ServiceFilter(typeof(SetAttributesViewBag))]
    public IActionResult Create()
    {
        return View(new PropertSaveViewModel());
    }

    public IActionResult Profile()
    {

        return View();
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
        vm.AgentId = Guid.Parse("325c8c63-d4cb-4038-924b-3acde9fdd969");
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

        vm.AgentId = Guid.Parse("325c8c63-d4cb-4038-924b-3acde9fdd969");
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
        if(vm.Pictures != null && vm.Pictures.Count > 0 )
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
            Console.WriteLine(ex.Message);
            return RedirectToAction("Index");
        }
    }


}
