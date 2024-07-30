using RealState.Application.DTOs.User;
using RealState.Application.Extras.ResultObject;
using RealState.Application.QueryFilters.User;
using RealState.Application.ViewModel.User;

namespace RealState.Application.Interfaces.Services
{
    public interface IUserServices
    {
        public Task<Result<Unit>> UpdateAsync(string userId, UserSaveViewModel userViewModel);

        public Task<Result<Unit>> DeleteAsync(string userId);

        public Task<Result<Unit>> RemoveRolesAsync(string userId, List<string> roleIds);

        public Task<Result<Unit>> AddRolesAsync(string userId, List<string> roleIds);

        public Task<Result<ApplicationUserDTO>> GetByIdAsync(string userId);

        public Result<List<ApplicationUserDTO>> GetAll(UserQueryFilter? filter);

        public Task<Result<Unit>> ChangeActiveStatus(string userId, bool status);
        Task<Result<Unit>>  UpdateAgent(string userId, UserSaveViewModel userSaveViewModel);
    }
}
