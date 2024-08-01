using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealState.Application.DTOs.Role;
using RealState.Application.Enums;
using RealState.Application.Interfaces.Repositories;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Repositories
{
    public class RoleRepository
        (
        IMapper mapper,
        RoleManager<ApplicationRole> roleManager
        )
        : IRoleRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

        public async Task<bool> CreateRoleAsync(ApplicationRoleDTO roleDto)
        {
            var role = _mapper.Map<ApplicationRole>(roleDto);
            var result = await _roleManager.CreateAsync(role);
            if(!result.Succeeded)
                return false;
            return true;
        }

        public async Task<bool> DeleteAsync(ApplicationRoleDTO roleDto)
        {
            var role = _mapper.Map<ApplicationRole>(roleDto);
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return false;
            return true;
        }

        public async Task<ApplicationRoleDTO?> GetAsync(string roleId)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x=>x.Id == roleId);
            return _mapper.Map<ApplicationRoleDTO>(role);
        }

        public IEnumerable<ApplicationRoleDTO> Get()
        {
            var roles = _roleManager.Roles.AsNoTracking()
                                          .AsEnumerable();
            return _mapper.Map<IEnumerable<ApplicationRoleDTO>>(roles);
        }

        public IEnumerable<ApplicationRoleDTO> GetBasicRoles()
        {
            string[] basicRoleNames = { RoleTypes.Client.ToString(), RoleTypes.StateAgent.ToString() };

            var roles = _roleManager.Roles.Where(x => basicRoleNames.Contains(x.Name))
                                          .AsNoTracking()
                                          .AsEnumerable();

            return _mapper.Map<IEnumerable<ApplicationRoleDTO>>(roles);
        }

        public async Task<ApplicationRoleDTO?> GetByNameAsync(string roleName)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == roleName);
            return _mapper.Map<ApplicationRoleDTO>(role);
        }

        public IEnumerable<ApplicationRoleDTO> GetManagementRoles()
        {
            string[] managementRoleNames = { RoleTypes.Admin.ToString(), RoleTypes.Developer.ToString() };

            var roles = _roleManager.Roles.Where(x => managementRoleNames.Contains(x.Name))
                                          .AsNoTracking()
                                          .AsEnumerable();

            return _mapper.Map<IEnumerable<ApplicationRoleDTO>>(roles);
        }

        public IEnumerable<ApplicationRoleDTO> GetRolesById(List<string> Ids)
        {
            var roles = _roleManager.Roles.Where(x=>Ids.Contains(x.Id))
                                          .AsNoTracking() 
                                          .AsEnumerable();
            return _mapper.Map<IEnumerable<ApplicationRoleDTO>>(roles);
        }

        public async Task<bool> UpdateAsync(ApplicationRoleDTO roleDto)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x=>x.Id == roleDto.Id);
            _mapper.Map(roleDto, role);
            var result = await _roleManager.UpdateAsync(role!);
            if (!result.Succeeded)
                return false;
            return true;
        }
    }
}
