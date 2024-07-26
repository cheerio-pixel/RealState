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
                .ReverseMap();

            CreateMap<ApplicationUser, SaveApplicationUserDTO>()
                .ForMember(des => des.ConfirmPassword, opt => opt.Ignore())
               .ReverseMap();
            #endregion

            #region Role
            CreateMap<ApplicationRole, ApplicationRoleDTO>()
               .ReverseMap();
            #endregion
        }
    }
}
