namespace SalesAndPurchase.Server.Application.Features.Suppliers.Commands
{
    public partial class EditSupplierCommand : PutSupplierRequest, ICommand<SupplierResponse?>
    {
    }

    public sealed class EditSupplierCommandHandler : ICommandHandler<EditSupplierCommand, SupplierResponse?>
    {
        private readonly IBaseRepositoryAsync<Supplier, string> _repo;
        private readonly IGenericUnitOfWork _genericUnitOfWork;

        public EditSupplierCommandHandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Supplier, string>();
        }

        public async ValueTask<SupplierResponse?> Handle(EditSupplierCommand command, CancellationToken cancellationToken)
        {
            await _genericUnitOfWork.BeginTransactionAsync();

            try
            {
                var entity = await _repo.GetByIdAsync(command.Id, cancellationToken);
                if (entity != null)
                {
                    entity.Id = command.Id;
                    entity.Name = command.Name ?? entity.Name;
                    entity.Address = command.Address;
                    entity.PhoneNumber = command.PhoneNumber;
                    entity.Active = command.Active;
                    var res = await _repo.UpdateAsync(entity, command.Id, cancellationToken);
                    await _genericUnitOfWork.Commit(cancellationToken);
                    return await MapToResponse(res);
                }
                return null;
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
    }
}
