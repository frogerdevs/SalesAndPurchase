namespace SalesAndPurchase.Server.Application.Features.Customers.Commands
{
    public partial class AddCustomerCommand : CustomerRequest, ICommand<CustomerResponse?>
    {
    }

    public sealed class AddCustomerCommandHandler : ICommandHandler<AddCustomerCommand, CustomerResponse?>
    {
        private readonly IBaseRepositoryAsync<Customer, string> _repo;
        private readonly IGenericUnitOfWork _genericUnitOfWork;
        public AddCustomerCommandHandler(IGenericUnitOfWork genericUnitOfWork)
        {
            _genericUnitOfWork = genericUnitOfWork;
            _repo = _genericUnitOfWork.Repository<Customer, string>();
        }

        public async ValueTask<CustomerResponse?> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
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

        private static Customer MapToEntity(AddCustomerCommand command)
        {
            return new Customer
            {
                Name=command.Name,
                Address=command.Address,
                PhoneNumber=command.PhoneNumber
            };
        }
    }

}
