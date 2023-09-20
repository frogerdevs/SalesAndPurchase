namespace SalesAndPurchase.Server.Application.Features.Customers.Queries
{
    public class GetCustomersQueries : IQuery<IEnumerable<CustomerResponse>>
    {
    }
    public sealed class GetCustomersQueriesHandler : IQueryHandler<GetCustomersQueries, IEnumerable<CustomerResponse>>
    {
        private readonly IBaseRepositoryAsync<Customer, string> _repo;

        public GetCustomersQueriesHandler(IBaseRepositoryAsync<Customer, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<IEnumerable<CustomerResponse>> Handle(GetCustomersQueries query, CancellationToken cancellationToken)
        {
            var items = await _repo.NoTrackingEntities.Select(i => new CustomerResponse
            {
                Id = i.Id,
                Name = i.Name!,
                PhoneNumber = i.PhoneNumber,
                Address = i.Address
            }).ToListAsync(cancellationToken);

            return items;
        }
    }
}
