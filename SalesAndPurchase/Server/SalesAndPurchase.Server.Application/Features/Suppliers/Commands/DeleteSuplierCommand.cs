namespace SalesAndPurchase.Server.Application.Features.Suppliers.Commands
{
    public class DeleteSuplierCommand : IRequest<bool>
    {
        public required string Id { get; set; }
    }
    public sealed class DeleteSuplierCommandhandler : IRequestHandler<DeleteSuplierCommand, bool>
    {
        private readonly IBaseRepositoryAsync<Supplier, string> _repo;
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        public DeleteSuplierCommandhandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Supplier, string>();
        }

        public async ValueTask<bool> Handle(DeleteSuplierCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the entity to be deleted from the database
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return false;
            }

            await _genericUnitOfWork.BeginTransactionAsync();

            try
            {
                // Remove the entity from the database
                entity.Active = false;
                await _repo.UpdateAsync(entity, request.Id, cancellationToken);
                ////await _repo.DeleteAsync(entity, cancellationToken);

                await _genericUnitOfWork.Commit(cancellationToken);
                return true; // Return a completed Task
            }
            catch (Exception)
            {
                await _genericUnitOfWork.Rollback();
                return false;
            }
        }
    }
}
