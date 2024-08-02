using AutoMapper;

using RealState.Application.DTOs.SalesType;
using RealState.Application.Enums;
using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Application.QueryFilters;
using RealState.Application.ViewModel.SalesType;
using RealState.Application.ViewModel.Upgrades;
using RealState.Domain.Entities;

namespace RealState.Application.Services
{
    internal class SalesTypesService
    : GenericService<SalesTypeSaveViewModel, SalesTypeViewModel, SalesTypes>, ISalesTypesService
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;

        public SalesTypesService(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        : base(salesTypeRepository, mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        protected override async IAsyncEnumerable<AppError> Validate(SalesTypeSaveViewModel vm, bool isUpdate)
        {
            if (await _salesTypeRepository.DoesSalesTypeNameExists(vm.Name, vm.Id))
            {
                yield return ErrorType.Conflict
                            .Because("Sales type's name already exists.")
                            .On(nameof(vm.Name));
            }
        }

        public async Task<List<SalesTypeListItemViewModel>> SearchSalesTypes(SalesTypesQueryFilter filter)
        {
            List<SalesTypesListItemDTO> salesTypes = await _salesTypeRepository.ListSalesTypesWithCount(filter);
            return _mapper.Map<List<SalesTypeListItemViewModel>>(salesTypes);
        }
    }
}