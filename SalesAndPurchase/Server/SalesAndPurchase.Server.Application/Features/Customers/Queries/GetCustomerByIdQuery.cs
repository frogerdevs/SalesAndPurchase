namespace SalesAndPurchase.Server.Application.Features.Customers.Queries
{
    public class GetCustomerByIdQuery : IQuery<CustomerResponse?>
    {
        public required string Id { get; set; }
    }
    public sealed class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, CustomerResponse?>
    {
        private readonly IBaseRepositoryAsync<Customer, string> _repo;

        public GetCustomerByIdQueryHandler(IBaseRepositoryAsync<Customer, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<CustomerResponse?> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            var items = await _repo.GetByIdAsync(query.Id, cancellationToken);

            return MapToResponse(items);
        }

        private static CustomerResponse? MapToResponse(Customer? items)
        {
            return items!=null ? new CustomerResponse
            {
                Id=items.Id,
                Name=items.Name,
                Address=items.Address,
                PhoneNumber=items.PhoneNumber
            } : null;
        }
    }

}
