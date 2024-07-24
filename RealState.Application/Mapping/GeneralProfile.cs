using AutoMapper;

using RealState.Application.Commands.Property.Create;
using RealState.Application.ViewModel.Property;
using RealState.Domain.Entities;

namespace RealState.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Property
            CreateMap<CreatePropertyCommand, Properties>().ReverseMap();
            CreateMap<PropertyViewModel, CreatePropertyCommand>().ReverseMap();
            #endregion
        }
    }
}
