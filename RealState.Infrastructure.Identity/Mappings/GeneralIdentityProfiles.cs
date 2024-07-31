using AutoMapper;

using RealState.Application.DTOs.Role;
using RealState.Application.DTOs.User;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Mappings
{
    public class GeneralIdentityProfiles : Profile
    {
        public GeneralIdentityProfiles()
        {
            #region User
            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ForMember(des => des.Active, opt => opt.MapFrom(org => org.EmailConfirmed))
                .ReverseMap()
                .ForMember(des => des.EmailConfirmed, opt => opt.MapFrom(org => org.Active));


            CreateMap<ApplicationUser, SaveApplicationUserDTO>()
                .ForMember(des => des.ConfirmPassword, opt => opt.Ignore())
                .ForMember(des => des.Active, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(des => des.EmailConfirmed, opt => opt.Ignore());
            #endregion

            #region Role
            CreateMap<ApplicationRole, ApplicationRoleDTO>()
               .ReverseMap();
            #endregion
        }
    }
}
