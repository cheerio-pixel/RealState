using AutoMapper;

using RealState.Application.DTOs.PropertyType;
using RealState.Application.ViewModel.PropertyType;
using RealState.Domain.Entities;

namespace RealState.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<PropertyTypeViewModel, PropertyTypes>().ReverseMap();
            CreateMap<PropertyTypeSaveViewModel, PropertyTypes>().ReverseMap();
            CreateMap<PropertyTypeListItemDTO, PropertyTypeSaveViewModel>().ReverseMap();
            CreateMap<PropertyTypeListItemDTO, PropertyTypeListItemViewModel>().ReverseMap();
        }
    }
}