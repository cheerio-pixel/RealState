using AutoMapper;

using RealState.Application.DTOs.User;
using RealState.Application.Enums;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters.User;
using RealState.Application.ViewModel.User;

namespace RealState.Application.Services
{
    public class UserServices(IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository) : IUserServices
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<Result<Unit>> AddRolesAsync(string userId, List<string> roleIds)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
                return ErrorType.Any.Because($"There isn`t any user with this id: {userId}");

            var roleNames = _roleRepository.GetRolesById(roleIds).Select(x=>x.Name).ToList();
            if (roleNames.Count() == 0)
                return ErrorType.Any.Because($"There isn`t any roles with those ids: {roleIds.ToString()}");

            var result = await _userRepository.AddRolesAsync(user, roleNames);
            if(!result)
                return ErrorType.Any.Because($"There is a problem adding roles to user");

            return Unit.T;
        }

        public async Task<Result<Unit>> DeleteAsync(string userId)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
                return ErrorType.Any.Because($"There isn`t any user with this id: {userId}");

            var result = await _userRepository.DeleteAsync(user);
            if (!result)
                return ErrorType.Any.Because($"There is a problem deleting user");

            return Unit.T;
        }

        public Result<List<ApplicationUserDTO>> GetAll(UserQueryFilter? filter)
        {
            IEnumerable<ApplicationUserDTO> users = [];

            if(filter is not null)
            {
                users = _userRepository.GetWithInclude(filter, ["Roles"]);
            }
            else
            {
                users = _userRepository.GetWithInclude(["Roles"]);
            }

            return users.ToList();
        }

        public async Task<Result<ApplicationUserDTO>> GetByIdAsync(string userId)
        {
            return await _userRepository.GetWithInclude(userId, ["Roles"]);
        }

        public async Task<Result<Unit>> RemoveRolesAsync(string userId, List<string> roleIds)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
                return ErrorType.Any.Because($"There isn`t any user with this id: {userId}");

            var roleNames = _roleRepository.GetRolesById(roleIds).Select(x => x.Name).ToList();
            if (roleNames.Count() == 0)
                return ErrorType.Any.Because($"There isn`t any roles with those ids: {roleIds.ToString()}");

            var result = await _userRepository.RemoveRolesAsync(user, roleNames);
            if (!result)
                return ErrorType.Any.Because($"There is a problem removing roles to user");

            return Unit.T;
        }

        public async Task<Result<Unit>> UpdateAsync(string userId, UserSaveViewModel userViewModel)
        {
            #region Validations
            var userById = await _userRepository.Get(userId);
            if (userById is null)
                return ErrorType.Any.Because($"There isn`t any user with this id: {userId}");

            var userByName = _userRepository.Get(new UserQueryFilter() { UserName = userViewModel.UserName }).FirstOrDefault();
            if (userByName is not null && userByName.Id != userId)
                return ErrorType.Any.Because($"This user name: {userViewModel.UserName} is already taken");

            var userByEmail = _userRepository.Get(new UserQueryFilter() { Email = userViewModel.Email }).FirstOrDefault();
            if (userByEmail is not null && userByEmail.Id != userId)
                return ErrorType.Any.Because($"This email: {userViewModel.Email} is already taken");

            var userByIdentifierCard = _userRepository.Get(new UserQueryFilter() { IdentifierCard = userViewModel.IdentifierCard }).FirstOrDefault();
            if (userByIdentifierCard is not null && userByIdentifierCard.Id != userId)
                return ErrorType.Any.Because($"This identifier Card: {userViewModel.IdentifierCard} is already taken");

            var IsActive = userById.Active;
            if (IsActive)
                return ErrorType.Any.Because($"This user isn`t active");
            #endregion

            var saveUser = _mapper.Map<SaveApplicationUserDTO>(userViewModel);
            var result = await _userRepository.UpdateAsync(saveUser);
            if (!result)
                return ErrorType.Any.Because($"There is a problem updating user");

            return Unit.T;
        }
    }
}
