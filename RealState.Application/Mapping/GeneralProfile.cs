using AutoMapper;

using RealState.Application.Commands.Authentication.Register;
using RealState.Application.Commands.PropertyType.Create;
using RealState.Application.Commands.PropertyType.Update;
using RealState.Application.DTOs.Account.Authentication;
using RealState.Application.DTOs.Account.ConfirmAccount;
using RealState.Application.DTOs.Account.ForgotPassword;
using RealState.Application.DTOs.Account.ResetPassword;
using RealState.Application.DTOs.Property;
using RealState.Application.DTOs.PropertyType;
using RealState.Application.DTOs.Role;
using RealState.Application.DTOs.SalesType;
using RealState.Application.DTOs.User;
using RealState.Application.ViewModel.Account;
using RealState.Application.ViewModel.Favorite;
using RealState.Application.ViewModel.Pictures;
using RealState.Application.ViewModel.PropertiesUpgrades;
using RealState.Application.ViewModel.Property;
using RealState.Application.ViewModel.PropertyType;
using RealState.Application.ViewModel.Role;
using RealState.Application.ViewModel.SalesType;
using RealState.Application.ViewModel.Upgrades;
using RealState.Application.ViewModel.User;
using RealState.Domain.Entities;

namespace RealState.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            #region Favorite
            CreateMap<FavoriteSaveViewModel, Favorite>().ReverseMap();
            CreateMap<FavoriteViewModel, Favorite>().ReverseMap();
            #endregion
            #region Property
            CreateMap<PropertSaveViewModel, Properties>().ForMember(x => x.Pictures, x => x.Ignore()).ReverseMap();
            CreateMap<PropertyViewModel, Properties>().ReverseMap();
            CreateMap<Properties, PropertyDetailsViewModel>()
                .ForMember(x => x.Upgrade, 
                opt => opt.MapFrom(src => src.PropertiesUpgrades.Select(x => x.Upgrade).ToList()));
            CreateMap<PropertyDTO, Properties>().ReverseMap();
            #endregion

            #region Picture
            CreateMap<Pictures, PicturesViewModel>().ReverseMap();
            CreateMap<Pictures, PicturesSaveViewModel>().ReverseMap();
            #endregion

            #region Property Type ViewModel
            CreateMap<PropertyTypeViewModel, PropertyTypes>().ReverseMap();
            CreateMap<PropertyTypeSaveViewModel, PropertyTypes>().ReverseMap();
            CreateMap<PropertyTypeListItemDTO, PropertyTypeSaveViewModel>().ReverseMap();
            CreateMap<PropertyTypeListItemDTO, PropertyTypeListItemViewModel>().ReverseMap();

            CreateMap<UpgradesViewModel, Upgrades>().ReverseMap();
            CreateMap<UpgradesSaveViewModel, Upgrades>().ReverseMap();

            CreateMap<SalesTypeViewModel, SalesTypes>().ReverseMap();
            CreateMap<SalesTypeSaveViewModel, SalesTypes>().ReverseMap();
            CreateMap<SalesTypeListItemViewModel, SalesTypesListItemDTO>().ReverseMap();
            #endregion

            #region Account
            CreateMap<ConfirmAccountViewModel, ConfirmAccountRequestDTO>().ReverseMap();
            CreateMap<ForgotPasswordViewModel, ForgotPasswordRequestDTO>().ReverseMap();
            CreateMap<ResetPasswordViewModel, ResetPasswordRequestDTO>().ReverseMap();
            CreateMap<LoginViewModel, AuthenticationRequestDTO>().ReverseMap();
            #endregion

            #region User
            CreateMap<UserSaveViewModel, SaveApplicationUserDTO>().ReverseMap();
            CreateMap<ApplicationUserDTO, SaveApplicationUserDTO>().ReverseMap();
            CreateMap<ApplicationUserDTO, UserSaveViewModel>().ReverseMap();
            CreateMap<ApplicationUserDTO, AgentDTO>().ReverseMap();
            CreateMap<ApplicationUserDTO, AgentSaveViewModel>().ForMember(x => x.Picture, x => x.Ignore()).ReverseMap();
            CreateMap<UserSaveViewModel, AgentSaveViewModel>().ForMember(x => x.Picture, x => x.Ignore()).ReverseMap();
            #endregion

            #region Role
            CreateMap<RoleViewModel, ApplicationRoleDTO>().ReverseMap();
            #endregion


            #region PropertyUpgrade
            CreateMap<PropertiesUpgrades, PropertyUpgradeSaveViewModel>().ReverseMap();
            CreateMap<PropertiesUpgrades, PropertyUpgradeViewModel>().ReverseMap();
            CreateMap<PropertSaveViewModel, PropertyUpgradeSaveViewModel>();
            CreateMap<UpgradesViewModel, Upgrades>().ReverseMap();
            #endregion

            CreateMap<PropertyTypeDTO, PropertyTypes>().ReverseMap();
            CreateMap<UpdatePropertyTypeCommand, PropertyTypes>().ReverseMap();
            CreateMap<UpdatePropertyTypeResponse, PropertyTypes>().ReverseMap();
            CreateMap<CreatePropertyTypeCommand, PropertyTypes>().ReverseMap();
            CreateMap<PropertyTypeDTO, PropertyTypeViewModel>().ReverseMap();

            CreateMap<SaveApplicationUserDTO, RegisterCommand>().ReverseMap();
        }
    }
}