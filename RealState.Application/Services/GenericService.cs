using AutoMapper;

using RealState.Application.Extras.ResultObject;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.Interfaces.Services;
using RealState.Domain.Common;

namespace RealState.Application.Services
{
    public class GenericService<TSaveViewModel, TViewModel, TEntity, TKey>(IGenericRepository<TEntity, TKey> genericRepository, IMapper mapper)
    : IGenericService<TSaveViewModel, TViewModel, TEntity, TKey>
    where TSaveViewModel : class
    where TViewModel : class
    where TEntity : Entity<TKey>
    {
        private readonly IGenericRepository<TEntity, TKey> _genericRepository = genericRepository;
        private readonly IMapper _mapper = mapper;
#pragma warning disable CS1998
        protected virtual async IAsyncEnumerable<AppError> Validate(TSaveViewModel vm, bool isUpdate)
#pragma warning restore CS1998
        {
            yield break;
        }

        public virtual async Task<Result<TSaveViewModel>> Add(TSaveViewModel vm)
        {
            Result<Unit> result = await Validate(vm, false).ToResult();
            if (result.IsSuccess)
            {
                TEntity data = _mapper.Map<TEntity>(vm);
                await _genericRepository.Create(data);
                vm = _mapper.Map<TSaveViewModel>(data);
                return vm;
            }
            return result.Map(_ => vm);
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

        public virtual async Task<Result<Unit>> Update(TSaveViewModel vm, TKey id)
        {
            Result<Unit> result = await Validate(vm, true).ToResult();
            if (result.IsSuccess)
            {
                TEntity entity = _mapper.Map<TEntity>(vm);
                entity.Id = id;
                await _genericRepository.Update(entity);
            }
            return result;
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