using AutoMapper;

using RealState.Application.Commands.Property.Create;
using RealState.Application.DTOs.PropertyType;
using RealState.Application.DTOs.SalesType;
using RealState.Application.DTOs.Account.Authentication;
using RealState.Application.DTOs.Account.ConfirmAccount;
using RealState.Application.DTOs.Account.ForgotPassword;
using RealState.Application.DTOs.Account.ResetPassword;
using RealState.Application.DTOs.Role;
using RealState.Application.DTOs.User;
using RealState.Application.ViewModel.Account;
using RealState.Application.ViewModel.PropertyType;
using RealState.Application.ViewModel.Property;
using RealState.Application.ViewModel.Role;
using RealState.Application.ViewModel.User;
using RealState.Domain.Entities;

using RealState.Application.ViewModel.Pictures;
using RealState.Application.ViewModel.SalesType;
using RealState.Application.ViewModel.Upgrades;
using RealState.Application.ViewModel.PropertiesUpgrades;

namespace RealState.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Property
            CreateMap<CreatePropertyCommand, Properties>().ReverseMap();
            CreateMap<PropertSaveViewModel, Properties>().ForMember(x => x.Pictures, x => x.Ignore()).ReverseMap();
            CreateMap<PropertyViewModel, CreatePropertyCommand>().ReverseMap();
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
            #endregion

            #region Role
            CreateMap<RoleViewModel, ApplicationRoleDTO>().ReverseMap();
            #endregion


            #region PropertyUpgrade
            CreateMap<PropertiesUpgrades, PropertyUpgradeSaveViewModel>().ReverseMap();
            CreateMap<PropertiesUpgrades, PropertyUpgradeViewModel>().ReverseMap();
            CreateMap<PropertSaveViewModel, PropertyUpgradeSaveViewModel>();
            #endregion

        }
    }
}