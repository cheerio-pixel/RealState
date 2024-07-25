using RealState.Application.Extras.ResultObject;
using RealState.Application.ViewModel.Property;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<PropertSaveViewModel, PropertyViewModel, Properties>
    {
        Task<Result<PropertyViewModel>> GetByIdWithPictures(Guid id);
    }
}