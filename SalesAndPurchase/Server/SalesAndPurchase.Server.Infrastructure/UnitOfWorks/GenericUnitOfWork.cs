using Microsoft.EntityFrameworkCore.Storage;

namespace SalesAndPurchase.Server.Infrastructure.UnitOfWorks
{
    public class GenericUnitOfWork : IGenericUnitOfWork
    {
        private bool disposed;
        private Hashtable? _repositories;
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        public GenericUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public ApplicationDbContext Context => _context;
        public async ValueTask BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public async ValueTask Commit(CancellationToken cancellationToken)
        {
            await _transaction.CommitAsync(cancellationToken);
            //return await _context.SaveChangesAsync(cancellationToken);
        }

        public async ValueTask<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            //foreach (var cacheKey in cacheKeys)
            //{
            //    _cache.Remove(cacheKey);
            //}
            return result;
        }

        public IBaseRepositoryAsync<T, TId> Repository<T, TId>() where T : class
        {
            _repositories ??= new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepositoryAsync<,>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _context);

                _repositories.Add(type, repositoryInstance);

            }

            return (IBaseRepositoryAsync<T, TId>)_repositories[type]!;
        }

        public ValueTask Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return ValueTask.CompletedTask;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed&&disposing)
            {
                //dispose managed resources
                _context.Dispose();
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}
