using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RealState.Application.DTOs.Role;
using RealState.Application.Interfaces.Repositories;
using RealState.Infrastructure.Identity.Context;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Repositories
{
    public class RoleRepository
        (
        IdentityContext context,
        IMapper mapper,
        RoleManager<ApplicationRole> roleManager
        )
        : IRoleRepository
    {
        private readonly IdentityContext _context = context;
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

        public async Task<ApplicationRoleDTO?> Get(string roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            return _mapper.Map<ApplicationRoleDTO>(role);
        }

        public IEnumerable<ApplicationRoleDTO> Get()
        {
            var roles = _context.Roles.AsEnumerable();
            return _mapper.Map<IEnumerable<ApplicationRoleDTO>>(roles);
        }

        public async Task<bool> UpdateAsync(ApplicationRoleDTO roleDto)
        {
            var role = await _context.Roles.FindAsync(roleDto.Id);
            _mapper.Map(roleDto, role);
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return false;
            return true;
        }
    }
}
