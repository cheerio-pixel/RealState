namespace RealState.Application.ViewModel
{
    public class BaseSaveViewModel<TKey>
    {
        public TKey? Id { get; set; }
    }

    public class BaseSaveViewModel
    : BaseSaveViewModel<Guid?>;
}