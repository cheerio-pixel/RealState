using RealState.Application.Extras.ResultObject;
using RealState.Domain.Common;

namespace RealState.Application.Interfaces.Services
{
    public interface IGenericService<TSaveViewModel, TViewModel, TEntity, TKey>
    where TSaveViewModel : class
    where TViewModel : class
    where TEntity : Entity<TKey>
    {
        Task<Result<Unit>> Update(TSaveViewModel vm, TKey id);

        Task<Result<TSaveViewModel>> Add(TSaveViewModel vm);

        Task Delete(TKey id);

        Task<TSaveViewModel?> GetByIdSaveViewModel(TKey id);

        Task<List<TViewModel>> GetAllViewModel();
    }

    public interface IGenericService<TSaveViewModel, TViewModel, TEntity>
    : IGenericService<TSaveViewModel, TViewModel, TEntity, Guid>
    where TSaveViewModel : class
    where TViewModel : class
    where TEntity : Entity<Guid>;
}