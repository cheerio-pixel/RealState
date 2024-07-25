using RealState.Application.DTOs.Role;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task<bool> CreateRoleAsync(ApplicationRoleDTO roleDto);

        public Task<bool> UpdateAsync(ApplicationRoleDTO roleDto);

        public Task<bool> DeleteAsync(ApplicationRoleDTO roleDto);

        public Task<ApplicationRoleDTO?> Get(string roleId);

        public IEnumerable<ApplicationRoleDTO> Get();
    }
}
