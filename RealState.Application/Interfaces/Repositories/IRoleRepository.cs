using RealState.Application.DTOs.Role;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task<bool> CreateRoleAsync(ApplicationRoleDTO roleDto);

        public Task<bool> UpdateAsync(ApplicationRoleDTO roleDto);

        public Task<bool> DeleteAsync(ApplicationRoleDTO roleDto);

        public Task<ApplicationRoleDTO?> GetAsync(string roleId);

        public IEnumerable<ApplicationRoleDTO> Get();

        public IEnumerable<ApplicationRoleDTO> GetBasicRoles();

        public IEnumerable<ApplicationRoleDTO> GetManagementRoles();

        public IEnumerable<ApplicationRoleDTO> GetRolesById(List<string> Ids);

        public Task<ApplicationRoleDTO?> GetByNameAsync(string roleName);
    }
}
