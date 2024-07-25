using RealState.Application.ViewModel.Property;
using RealState.Domain.Entities;

namespace RealState.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<PropertSaveViewModel, PropertyViewModel, Properties>
    {
    }
}