namespace SalesAndPurchase.Server.Application.Features.Customers.Commands
{
    public partial class EditCustomerCommand : PutCustomerRequest, ICommand<CustomerResponse?>
    {
    }

    public sealed class EditCustomerCommandHandler : ICommandHandler<EditCustomerCommand, CustomerResponse?>
    {
        private readonly IBaseRepositoryAsync<Customer, string> _repo;
        private readonly IGenericUnitOfWork _genericUnitOfWork;

        public EditCustomerCommandHandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Customer, string>();
        }

        public async ValueTask<CustomerResponse?> Handle(EditCustomerCommand command, CancellationToken cancellationToken)
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

        private static ValueTask<CustomerResponse> MapToResponse(Customer res)
        {
            return new ValueTask<CustomerResponse>(new CustomerResponse
            {
                Id=res.Id,
                Name=res.Name,
                Address=res.Address,
                PhoneNumber=res.PhoneNumber
            });
        }
    }

}
