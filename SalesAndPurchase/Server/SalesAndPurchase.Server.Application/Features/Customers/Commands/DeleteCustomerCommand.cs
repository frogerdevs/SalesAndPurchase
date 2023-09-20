namespace SalesAndPurchase.Server.Application.Features.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public required string Id { get; set; }
    }
    public sealed class DeleteCustomerCommandhandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly IBaseRepositoryAsync<Customer, string> _repo;
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        public DeleteCustomerCommandhandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Customer, string>();
        }

        public async ValueTask<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
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
