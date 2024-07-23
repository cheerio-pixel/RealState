using AutoMapper;

using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Domain.Common;

namespace RealState.Application.Services
{
    public class GenericService<TSaveViewModel, TViewModel, TEntity, TKey>
    : IGenericService<TSaveViewModel, TViewModel, TEntity, TKey>
    where TSaveViewModel : class
    where TViewModel : class
    where TEntity : Entity<TKey>
    {
        private readonly IGenericRepository<TEntity, TKey> _genericRepository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity, TKey> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public virtual async Task<TSaveViewModel> Add(TSaveViewModel vm)
        {
            TEntity data = _mapper.Map<TEntity>(vm);
            await _genericRepository.Create(data);
            vm = _mapper.Map<TSaveViewModel>(data);
            // var viewModelIdProperty = vm.GetType().GetProperty("Id");
            // viewModelIdProperty?.SetValue(vm, data.GetType().GetProperty("Id"));
            return vm;
        }

        public virtual async Task Delete(TKey id)
        {
            await _genericRepository.Delete(id);
        }

        public virtual async Task<List<TViewModel>> GetAllViewModel()
        {
            List<TEntity> entities = await _genericRepository.GetAll();
            return _mapper.Map<List<TEntity>, List<TViewModel>>(entities);
        }

        public virtual async Task<TSaveViewModel?> GetByIdSaveViewModel(TKey id)
        {
            TEntity? entity = await _genericRepository.GetById(id);
            if (entity is not null)
            {
                return _mapper.Map<TSaveViewModel?>(entity);
            }
            return null;
        }

        public virtual async Task Update(TSaveViewModel vm, TKey id)
        {
            TEntity entity = _mapper.Map<TEntity>(vm);
            entity.Id = id;
            await _genericRepository.Update(entity);
        }
    }

    public class GenericService<TSaveViewModel, TViewModel, TEntity>
    : GenericService<TSaveViewModel, TViewModel, TEntity, Guid>,
      IGenericService<TSaveViewModel, TViewModel, TEntity>
    where TSaveViewModel : class
    where TViewModel : class
    where TEntity : Entity<Guid>
    {
        public GenericService(IGenericRepository<TEntity, Guid> genericRepository, IMapper mapper)
        : base(genericRepository, mapper)
        {
        }
    }
}