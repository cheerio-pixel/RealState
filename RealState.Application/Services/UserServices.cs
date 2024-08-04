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
    public class UserServices(IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository, IPropertyService propertyService) : IUserServices
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IPropertyService _propertyService = propertyService;

        public async Task<Result<Unit>> AddRolesAsync(string userId, List<string> roleIds)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
                return $"There isn`t any user with this id: {userId}";

            var roleNames = _roleRepository.GetRolesById(roleIds).Select(x => x.Name).ToList();
            if (roleNames.Count == 0)
                return $"There isn`t any roles with those ids: {roleIds}";

            var result = await _userRepository.AddRolesAsync(user, roleNames);
            return result ? Unit.T : "There is a problem adding roles to user";
        }

        public async Task<Result<Unit>> ChangeStatusAsync(string userId, string currentUserId, bool status)
        {
            var user = await _userRepository.GetWithInclude(userId, ["Roles"]);
            if (user is null)
                return $"There isn`t any user with this id: {userId}";

            if (userId == currentUserId)
            {
                var isAdmin = user.Roles.Any(x => x.Name == nameof(RoleTypes.Admin));
                if (isAdmin)
                    return "You can't update yourself";
            }

            var result = await _userRepository.ChangeStatus(userId, status);
            if (!result)
                return "There is a problem updating user";

            return Unit.T;
        }

        public async Task<Result<Unit>> DeleteAsync(string userId)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
                return $"There isn`t any user with this id: {userId}";

            var result = await _userRepository.DeleteAsync(user);
            if (!result)
            {
                return "There is a problem deleting user";
            }
            else if (user.Roles.Select(a => a.Name).Contains(nameof(RoleTypes.StateAgent)))
            {
                await _propertyService.DeletePropertiesOfAgent(Guid.Parse(userId));
            }

            return Unit.T;
        }

        public Result<List<ApplicationUserDTO>> GetAll(UserQueryFilter? filter)
        {
            IEnumerable<ApplicationUserDTO> users = filter switch
            {
                not null => _userRepository.GetWithInclude(filter, ["Roles"]),
                _ => _userRepository.GetWithInclude(["Roles"])
            };

            return users.ToList();
        }

        public async Task<Result<ApplicationUserDTO?>> GetByIdAsync(string userId)
        {
            return await _userRepository.GetWithInclude(userId, ["Roles"]);
        }

        public async Task<Result<Unit>> RemoveRolesAsync(string userId, List<string> roleIds)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
                return $"There isn`t any user with this id: {userId}";

            var roleNames = _roleRepository.GetRolesById(roleIds).Select(x => x.Name).ToList();
            if (roleNames.Count == 0)
                return $"There isn`t any roles with those ids: {roleIds}";

            var result = await _userRepository.RemoveRolesAsync(user, roleNames);
            if (!result)
                return "There is a problem removing roles to user";

            return Unit.T;
        }

        public async Task<Result<Unit>> UpdateAsync(string userId, string currentUserId, UserSaveViewModel userViewModel)
        {
            #region Validations
            var userById = await _userRepository.GetWithInclude(userId, ["Roles"]);
            if (userById is null)
                return $"There isn`t any user with this id: {userId}";

            if (userId == currentUserId)
            {
                var isAdmin = userById.Roles.Any(x => x.Name == nameof(RoleTypes.Admin));
                if (isAdmin)
                    return "You can't update yourself";
            }

            var userByName = _userRepository.Get(new UserQueryFilter() { UserName = userViewModel.UserName }).FirstOrDefault();
            if (userByName is not null && userByName.Id != userId)
                return $"This user name: {userViewModel.UserName} is already taken";

            var userByEmail = _userRepository.Get(new UserQueryFilter() { Email = userViewModel.Email }).FirstOrDefault();
            if (userByEmail is not null && userByEmail.Id != userId)
                return $"This email: {userViewModel.Email} is already taken";

            var userByIdentifierCard = _userRepository.Get(new UserQueryFilter() { IdentifierCard = userViewModel.IdentifierCard }).FirstOrDefault();
            if (userByIdentifierCard is not null && userByIdentifierCard.Id != userId)
                return $"This identifier Card: {userViewModel.IdentifierCard} is already taken";

            var user = await _userRepository.Get(userId);
            userViewModel.Picture ??= user!.Picture;

            var IsActive = userById.Active;
            if (!IsActive)
                return "This user isn`t active";
            #endregion

            var saveUser = _mapper.Map<SaveApplicationUserDTO>(userViewModel);
            var result = await _userRepository.UpdateAsync(saveUser);
            if (!result)
                return "There is a problem updating user";

            return Unit.T;
        }

        public async Task<Result<Unit>> UpdateAgent(string userId, UserSaveViewModel userSaveViewModel)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
                return "There isn't any user";

            if (userSaveViewModel.Picture != "")
            {
                user.Picture = userSaveViewModel.Picture!;
            }
            user.FirstName = userSaveViewModel.FirstName;
            user.LastName = userSaveViewModel.LastName;
            user.PhoneNumber = userSaveViewModel.PhoneNumber!;

            var result = await _userRepository.UpdateAsync(_mapper.Map<SaveApplicationUserDTO>(user));
            return result ? Unit.T : "There is a problem updating user";
        }

        public async Task<UserStatisticsDto> UserStatisticsDtoAsync()
        {
            return await _userRepository.GetUserStatisticsAsync();
        }
   }
}
