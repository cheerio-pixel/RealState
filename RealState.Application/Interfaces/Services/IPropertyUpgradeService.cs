using RealState.Application.Extras.ResultObject;
using RealState.Application.ViewModel.PropertiesUpgrades;

namespace RealState.Application.Interfaces.Services
{
    public interface IPropertyUpgradeService
    {
        Task<Result<PropertyUpgradeSaveViewModel>> Add(PropertyUpgradeSaveViewModel vm);
        Task<List<PropertyUpgradeViewModel>> GetAllByPropertyId(Guid propertyId);
        Task UpdatePropertyUpgradesByPropertyId(PropertyUpgradeSaveViewModel vm, Guid propertyId);
        Task DeleteByPropertyId(Guid propertyId);
    }
}