using AutoMapper;

using RealState.Application.DTOs.Account.Authentication;
using RealState.Application.DTOs.Account.ConfirmAccount;
using RealState.Application.DTOs.Account.ForgotPassword;
using RealState.Application.DTOs.Account.ResetPassword;
using RealState.Application.DTOs.PropertyType;
using RealState.Application.DTOs.Role;
using RealState.Application.DTOs.User;
using RealState.Application.ViewModel.Account;
using RealState.Application.ViewModel.PropertyType;
using RealState.Application.Commands.Property.Create;
using RealState.Application.ViewModel.Property;
using RealState.Application.ViewModel.Role;
using RealState.Application.ViewModel.User;
using RealState.Domain.Entities;
using RealState.Application.ViewModel.Pictures;

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

            CreateMap<ConfirmAccountViewModel, ConfirmAccountRequestDTO>().ReverseMap();
            CreateMap<ForgotPasswordViewModel, ForgotPasswordRequestDTO>().ReverseMap();
            CreateMap<UserSaveViewModel, SaveApplicationUserDTO>().ReverseMap();
            CreateMap<ResetPasswordViewModel, ResetPasswordRequestDTO>().ReverseMap();
            CreateMap<LoginViewModel, AuthenticationRequestDTO>().ReverseMap();

            CreateMap<RoleViewModel, ApplicationRoleDTO>().ReverseMap();
        }
    }
}