using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using RealState.Application.DTOs.User;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.QueryFilters.User;
using RealState.Infrastructure.Identity.Context;
using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Repositories
{
    public class UserRepository
        (
        IdentityContext context,
        IMapper mapper,
        UserManager<ApplicationUser> userManager
        )
        : IUserRepository
    {
        private readonly IdentityContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<bool> AddRolesAsync(ApplicationUserDTO userDto, List<string> roles)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);

            var result = await _userManager.AddToRolesAsync(user, roles).ConfigureAwait(false);
            if (!result.Succeeded)
                return false;
            return true;
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            _context.Users.Remove(user);
            try
            {
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ApplicationUserDTO?> Get(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return _mapper.Map<ApplicationUserDTO?>(user);
        }

        public IEnumerable<ApplicationUserDTO> Get()
        {
            var user = _context.Users.AsEnumerable();
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(user);
        }

        public IEnumerable<ApplicationUserDTO> Get(UserQueryFilter filters)
        {
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(FilterQuery(filters).AsEnumerable());
        }

        public async Task<ApplicationUserDTO> GetWithInclude(string userId, List<string> properties)
        {
            var query = _context.Users.AsQueryable();

            foreach (var item in properties)
            {
                query = query.Include(item);
            }
            var user = await query.FirstOrDefaultAsync(x => x.Id == userId);
            return _mapper.Map<ApplicationUserDTO>(user);
        }

        public IEnumerable<ApplicationUserDTO> GetWithInclude(List<string> properties)
        {
            var query = _context.Users.AsQueryable();

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(query.AsEnumerable());
        }

        public IEnumerable<ApplicationUserDTO> GetWithInclude(UserQueryFilter filters, List<string> properties)
        {
            var query = FilterQuery(filters);

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(query.AsEnumerable());
        }

        public async Task<bool> RemoveRolesAsync(ApplicationUserDTO userDto, List<string> roles)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);

            var result = await _userManager.RemoveFromRolesAsync(user, roles).ConfigureAwait(false);
            if (!result.Succeeded)
                return false;
            return true;
        }

        public async Task<bool> UpdateAsync(SaveApplicationUserDTO userDto)
        {
            var user = await _context.Users.FindAsync(userDto.Id);
            _mapper.Map(userDto, user);
            _context.Users.Update(user);
            try
            {
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private IQueryable<ApplicationUser> FilterQuery(UserQueryFilter filters)
        {
            var query = _context.Users.AsQueryable();

            if (filters.FirstName is not null)
                query = query.Where(u => u.FirstName.Contains(filters.FirstName));

            if (filters.LastName is not null)
                query = query.Where(u => u.LastName.Contains(filters.LastName));

            if (filters.UserName is not null)
                query = query.Where(u => u.UserName.Contains(filters.UserName));

            if (filters.Email is not null)
                query = query.Where(u => u.Email.Contains(filters.Email));

            if (filters.IdentifierCard is not null)
                query = query.Where(u => u.IdentifierCard.Contains(filters.IdentifierCard));

            if (filters.PhoneNumber is not null)
                query = query.Where(u => u.PhoneNumber.Contains(filters.PhoneNumber));

            if (filters.Role is not null)
                query = query.Include(x => x.Roles)
                             .Where(u => u.Roles.Select(r => r.Name).Contains(filters.Role.ToString()));

            return query;
        }
    }
}
