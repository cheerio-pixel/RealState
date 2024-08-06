using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealState.Application.DTOs.User;
using RealState.Application.Enums;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.QueryFilters.User;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Repositories
{
    public class UserRepository
        (
        IMapper mapper,
        UserManager<ApplicationUser> userManager
        )
        : IUserRepository
    {
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<bool> AddRolesAsync(ApplicationUserDTO userDto, List<string> roleNames)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);

            var result = await _userManager.AddToRolesAsync(user, roleNames).ConfigureAwait(false);
            if (!result.Succeeded)
                return false;
            return true;
        }

        public async Task<bool> ChangeStatus(string userId, bool status)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            user!.EmailConfirmed = status;

            var result = await _userManager.UpdateAsync(user!);
            if (!result.Succeeded)
                return false;

            return true;
        }

        public async Task<bool> DeleteAsync(ApplicationUserDTO userDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userDto.Id);
            var result = await _userManager.DeleteAsync(user!);
            if (!result.Succeeded)
                return false;
            return true;
        }

        public async Task<ApplicationUserDTO?> Get(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return _mapper.Map<ApplicationUserDTO?>(user);
        }

        public IEnumerable<ApplicationUserDTO> Get()
        {
            var user = _userManager.Users.AsEnumerable();
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(user);
        }

        public IEnumerable<ApplicationUserDTO> Get(UserQueryFilter filters)
        {
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(FilterQuery(filters).AsEnumerable());
        }

        public async Task<ApplicationUserDTO?> GetWithInclude(string userId, List<string> properties)
        {
            var query = _userManager.Users.AsQueryable().AsNoTracking();

            foreach (var item in properties)
            {
                query = query.Include(item);
            }
            var user = await query.FirstOrDefaultAsync(x => x.Id == userId);
            return _mapper.Map<ApplicationUserDTO>(user);
        }

        public IEnumerable<ApplicationUserDTO> GetWithInclude(List<string> properties)
        {
            var query = _userManager.Users.AsQueryable().AsNoTracking();

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(query.AsEnumerable());
        }

        public IEnumerable<ApplicationUserDTO> GetWithInclude(UserQueryFilter filters, List<string> properties)
        {
            var query = FilterQuery(filters).AsNoTracking();

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(query.AsEnumerable());
        }

        public async Task<bool> RemoveRolesAsync(ApplicationUserDTO userDto, List<string> roleNames)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);

            var result = await _userManager.RemoveFromRolesAsync(user, roleNames).ConfigureAwait(false);
            if (!result.Succeeded)
                return false;
            return true;
        }

        public async Task<bool> UpdateAsync(SaveApplicationUserDTO userDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userDto.Id);
            _mapper.Map(userDto, user);
            var result = await _userManager.UpdateAsync(user!);
            if (!result.Succeeded)
                return false;

            if (userDto.Password is not null)
            {
                result = await ChangePassword(user!, userDto.Password);
                if (!result.Succeeded)
                    return false;
            }

            return true;
        }

        private async Task<IdentityResult> ChangePassword(ApplicationUser user, string newPassword)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);
            return result;
        }

        private IQueryable<ApplicationUser> FilterQuery(UserQueryFilter filters)
        {
            var query = _userManager.Users.AsQueryable();

            if (filters.FirstName is not null)
                query = query.Where(u => u.FirstName.Contains(filters.FirstName));

            if (filters.LastName is not null)
                query = query.Where(u => u.LastName.Contains(filters.LastName));

            if (filters.UserName is not null)
                query = query.Where(u => u.UserName!.Contains(filters.UserName));

            if (filters.Email is not null)
                query = query.Where(u => u.Email!.Contains(filters.Email));

            if (filters.IdentifierCard is not null)
                query = query.Where(u => u.IdentifierCard.Contains(filters.IdentifierCard));

            if (filters.PhoneNumber is not null)
                query = query.Where(u => u.PhoneNumber!.Contains(filters.PhoneNumber));

            if (filters.Active is not null)
                query = query.Where(u => u.EmailConfirmed == filters.Active);

            if (filters.Role is not null)
                query = query.Include(x => x.Roles)
                             .Where(u => u.Roles.Select(r => r.Name).Contains(filters.Role.ToString()));

            return query;
        }

        public async Task<UserStatisticsDto> GetUserStatisticsAsync()
        {
            var userStatistics = new UserStatisticsDto
            {
                // Obtener conteos directamente desde la base de datos utilizando IQueryable
                AdminActive = await _userManager.Users
                .Where(u => u.EmailConfirmed && u.Roles.Any(r => r.Name == nameof(RoleTypes.Admin)))
                .CountAsync(),

                AdminInactive = await _userManager.Users
                .Where(u => !u.EmailConfirmed && u.Roles.Any(r => r.Name == nameof(RoleTypes.Admin)))
                .CountAsync(),

                ClientActive = await _userManager.Users
                .Where(u => u.EmailConfirmed && u.Roles.Any(r => r.Name == nameof(RoleTypes.Client)))
                .CountAsync(),

                ClientInactive = await _userManager.Users
                .Where(u => !u.EmailConfirmed && u.Roles.Any(r => r.Name == nameof(RoleTypes.Client)))
                .CountAsync(),

                AgentActive = await _userManager.Users
                .Where(u => u.EmailConfirmed && u.Roles.Any(r => r.Name == nameof(RoleTypes.StateAgent)))
                .CountAsync(),

                AgentInactive = await _userManager.Users
                .Where(u => !u.EmailConfirmed && u.Roles.Any(r => r.Name == nameof(RoleTypes.StateAgent)))
                .CountAsync(),

                DeveloperActive = await _userManager.Users
                .Where(u => u.EmailConfirmed && u.Roles.Any(r => r.Name == nameof(RoleTypes.Developer)))
                .CountAsync(),

                DeveloperInactive = await _userManager.Users
                .Where(u => !u.EmailConfirmed && u.Roles.Any(r => r.Name == nameof(RoleTypes.Developer)))
                .CountAsync()
            };

            return userStatistics;
        }
    }
}
