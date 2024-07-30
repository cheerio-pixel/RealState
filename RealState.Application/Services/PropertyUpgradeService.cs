using AutoMapper;
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

        public async Task UpdatePropertyUpgradesByPropertyId(PropertyUpgradeSaveViewModel vm, Guid propertyId)
        {
            // Obtén la lista actual de mejoras para la propiedad desde la base de datos
            List<PropertiesUpgrades> currentUpgrades = await _propertyUpgradeRepository.GetAllByPropertyId(propertyId);

            // Supongamos que vm.UpgradeId es una lista de IDs de mejoras que deseas mantener
            var newUpgradeIds = vm.UpgradeId;

            // Identifica las mejoras que están en la base de datos pero no en la nueva lista (para eliminar)
            var upgradesToDelete = currentUpgrades
                .Where(upgrade => !newUpgradeIds!.Contains(upgrade.Id))
                .ToList();

            // Identifica las mejoras que están en la nueva lista pero no en la base de datos (para agregar)
            var existingUpgradeIds = currentUpgrades.Select(upgrade => upgrade.Id).ToList();
            var upgradesToAdd = newUpgradeIds!
                .Where(id => !existingUpgradeIds.Contains(id))
                .ToList();

            // Elimina las mejoras que ya no están en la nueva lista
            foreach (var upgrade in upgradesToDelete)
            {
                await _propertyUpgradeRepository.Delete(upgrade.Id);
            }

            // Agrega las mejoras que no estaban en la base de datos
            foreach (var upgradeId in upgradesToAdd)
            {
                PropertiesUpgrades propertyUpgrade = new()
                {
                    UpgradeId = upgradeId,
                    PropertyId = propertyId
                };
                await _propertyUpgradeRepository.Create(propertyUpgrade);
            }
        }
   
        public async Task DeleteByPropertyId(Guid propertyId)
        {
            List<PropertiesUpgrades> upgrades = await _propertyUpgradeRepository.GetAllByPropertyId(propertyId);
            foreach (var upgrade in upgrades)
            {
                await _propertyUpgradeRepository.Delete(upgrade.Id);
            }
        }
    }


}
