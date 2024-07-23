using RealState.Application.DTOs.User;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> UpdateAsync(SaveApplicationUserDTO user);

        public Task<bool> DeleteAsync(string userId);

        public Task<ApplicationUserDTO> GetByIdAsync(string userId);

        public IEnumerable<ApplicationUserDTO> GetAll();
    }
}
