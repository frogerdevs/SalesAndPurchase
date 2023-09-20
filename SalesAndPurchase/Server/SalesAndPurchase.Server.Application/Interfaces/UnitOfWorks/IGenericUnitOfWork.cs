namespace SalesAndPurchase.Server.Application.Interfaces.UnitOfWorks
{
    public interface IGenericUnitOfWork : IDisposable
    {
        ValueTask BeginTransactionAsync();
        IBaseRepositoryAsync<T, TId> Repository<T, TId>() where T : class;

        ValueTask Commit(CancellationToken cancellationToken);

        ValueTask<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

        ValueTask Rollback();

    }
}
