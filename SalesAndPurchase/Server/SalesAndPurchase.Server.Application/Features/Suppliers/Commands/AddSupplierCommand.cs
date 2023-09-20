namespace SalesAndPurchase.Server.Application.Features.Suppliers.Commands
{
    public partial class AddSupplierCommand : SupplierRequest, ICommand<SupplierResponse?>
    {
    }

    public sealed class AddSupplierCommandHandler : ICommandHandler<AddSupplierCommand, SupplierResponse?>
    {
        private readonly IBaseRepositoryAsync<Supplier, string> _repo;
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        public AddSupplierCommandHandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Supplier, string>();
        }

        public async ValueTask<SupplierResponse?> Handle(AddSupplierCommand command, CancellationToken cancellationToken)
        {
            var entity = MapToEntity(command);
            await _genericUnitOfWork.BeginTransactionAsync();

            try
            {
                var res = await _repo.AddAsync(entity, cancellationToken);
                await _genericUnitOfWork.Commit(cancellationToken);
                return await MapToResponse(res);
            }
            catch (Exception)
            {
                await _genericUnitOfWork.Rollback();
                return null;
            }
        }

        private static ValueTask<SupplierResponse> MapToResponse(Supplier res)
        {
            return new ValueTask<SupplierResponse>(new SupplierResponse
            {
                Name=res.Name,
                Address=res.Address,
                PhoneNumber=res.PhoneNumber,
                Id=res.Id,
                Active=res.Active
            });
        }

        private static Supplier MapToEntity(AddSupplierCommand command)
        {
            return new Supplier
            {
                Name=command.Name,
                Address=command.Address,
                PhoneNumber=command.PhoneNumber,
                Active=command.Active
            };
        }
    }

}
