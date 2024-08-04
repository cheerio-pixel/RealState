using RealState.Application.DTOs.User;
using RealState.Application.QueryFilters.User;

namespace RealState.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> UpdateAsync(SaveApplicationUserDTO user);

        public Task<bool> DeleteAsync(ApplicationUserDTO userDto);

        public Task<bool> ChangeStatus(string userId, bool status);

        public Task<bool> AddRolesAsync(ApplicationUserDTO userDto, List<string> roleNames);

        public Task<bool> RemoveRolesAsync(ApplicationUserDTO userDto, List<string> roleNames);

        public Task<ApplicationUserDTO?> Get(string userId);

        public IEnumerable<ApplicationUserDTO> Get();

        public IEnumerable<ApplicationUserDTO> Get(UserQueryFilter filters);

        public Task<ApplicationUserDTO?> GetWithInclude(string userId, List<string> properties);

        public IEnumerable<ApplicationUserDTO> GetWithInclude(List<string> properties);

        public IEnumerable<ApplicationUserDTO> GetWithInclude(UserQueryFilter filters, List<string> properties);
        Task<UserStatisticsDto> GetUserStatisticsAsync();
    }
}
