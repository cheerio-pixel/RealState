using RealState.Application.Extras.ResultObject;
using RealState.Application.ViewModel.Role;

namespace RealState.Application.Interfaces.Services
{
    public interface IRoleServices
    {
        public Result<List<RoleViewModel>> Get();

        public Result<List<RoleViewModel>> GetBasicRoles();

        public Result<List<RoleViewModel>> GetManagementRoles();
    }
}
