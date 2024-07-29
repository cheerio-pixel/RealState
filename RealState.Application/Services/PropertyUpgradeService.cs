using System;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.PropertiesUpgrades;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    public class PropertyUpgradeService(IPropertyUpgradeRepository propertyUpgradeRepository, IMapper mapper) : GenericService<PropertyUpgradeSaveViewModel, PropertyUpgradeViewModel, PropertiesUpgrades>(propertyUpgradeRepository, mapper), IPropertyUpgradeService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPropertyUpgradeRepository _propertyUpgradeRepository = propertyUpgradeRepository;
        
        public async Task<List<PropertyUpgradeViewModel>> GetAllByPropertyId(Guid propertyId)
        {
            List<PropertiesUpgrades> entities = await _propertyUpgradeRepository.GetAllByPropertyId(propertyId);
            return _mapper.Map<List<PropertiesUpgrades>, List<PropertyUpgradeViewModel>>(entities);
        }

        public override async Task<Result<PropertyUpgradeSaveViewModel>> Add(PropertyUpgradeSaveViewModel vm)
        {

            foreach (var upgrade in vm.UpgradeId!)
            {
                PropertiesUpgrades propertyUpgrade = new()
                {
                    UpgradeId = upgrade,
                    PropertyId = vm.PropertyId
                };
                await _propertyUpgradeRepository.Create(propertyUpgrade);
            }
            return Result.Ok(vm);


        }
    }


}
