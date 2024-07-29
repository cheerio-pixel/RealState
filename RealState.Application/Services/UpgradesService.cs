using AutoMapper;

using RealState.Application.Enums;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.Upgrades;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    internal class UpgradesService
    : GenericService<UpgradesSaveViewModel, UpgradesViewModel, Upgrades>, IUpgradesService
    {
        private readonly IUpgradesRepository _upgradesRepository;
        private readonly IMapper _mapper;

        public UpgradesService(IUpgradesRepository upgradesRepository, IMapper mapper)
        : base(upgradesRepository, mapper)
        {
            _upgradesRepository = upgradesRepository;
            _mapper = mapper;
        }

        protected override async IAsyncEnumerable<AppError> Validate(UpgradesSaveViewModel vm, bool isUpdate)
        {
            if (await _upgradesRepository.DoesUpgradeNameExists(vm.Name, vm.Id))
            {
                yield return ErrorType.Conflict
                            .Because("Upgrade's name already exists.")
                            .On(nameof(vm.Name));
            }
        }

        public async Task<List<UpgradesViewModel>> SearchUpgrades(UpgradesQueryFilter filter)
        {
            List<Upgrades> upgrades = await _upgradesRepository.ListUpgrades(filter);
            return _mapper.Map<List<UpgradesViewModel>>(upgrades);
        }
    }
}