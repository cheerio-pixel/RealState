using AutoMapper;

using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.ViewModel.Role;

namespace RealState.Application.Services
{
    public class RoleServices(IRoleRepository roleRepository, IMapper mapper) : IRoleServices
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IMapper _mapper = mapper;

        public Result<List<RoleViewModel>> Get()
        {
            var roles =  _roleRepository.Get().ToList();
            return _mapper.Map<List<RoleViewModel>>(roles);
        }

        public Result<List<RoleViewModel>> GetBasicRoles()
        {
            var roles = _roleRepository.GetBasicRoles().ToList();
            return _mapper.Map<List<RoleViewModel>>(roles);
        }

        public Result<List<RoleViewModel>> GetManagementRoles()
        {
            var roles = _roleRepository.GetManagementRoles().ToList();
            return _mapper.Map<List<RoleViewModel>>(roles);
        }
    }
}
