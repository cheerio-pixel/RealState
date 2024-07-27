using AutoMapper;

using RealState.Application.Commands.Property.Create;
using RealState.Application.DTOs.PropertyType;
using RealState.Application.DTOs.SalesType;
using RealState.Application.ViewModel.Pictures;
using RealState.Application.ViewModel.Property;
using RealState.Application.ViewModel.PropertyType;
using RealState.Application.ViewModel.SalesType;
using RealState.Application.ViewModel.Upgrades;
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

            #region Picture
            CreateMap<Pictures, PicturesViewModel>().ReverseMap();
            CreateMap<Pictures, PicturesSaveViewModel>().ReverseMap();
            #endregion

            CreateMap<PropertyTypeViewModel, PropertyTypes>().ReverseMap();
            CreateMap<PropertyTypeSaveViewModel, PropertyTypes>().ReverseMap();
            CreateMap<PropertyTypeListItemDTO, PropertyTypeSaveViewModel>().ReverseMap();
            CreateMap<PropertyTypeListItemDTO, PropertyTypeListItemViewModel>().ReverseMap();

            CreateMap<UpgradesViewModel, Upgrades>().ReverseMap();
            CreateMap<UpgradesSaveViewModel, Upgrades>().ReverseMap();

            CreateMap<SalesTypeViewModel, SalesTypes>().ReverseMap();
            CreateMap<SalesTypeSaveViewModel, SalesTypes>().ReverseMap();
            CreateMap<SalesTypeListItemViewModel, SalesTypesListItemDTO>().ReverseMap();
        }
    }
}