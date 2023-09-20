namespace SalesAndPurchase.Server.Application.Features.Suppliers.Queries
{
    public class GetSuppliersQuery : IQuery<IEnumerable<SupplierResponse>>
    {
    }
    public sealed class GetSuppliersQueryHandler : IQueryHandler<GetSuppliersQuery, IEnumerable<SupplierResponse>>
    {
        private readonly IBaseRepositoryAsync<Supplier, string> _repo;

        public GetSuppliersQueryHandler(IBaseRepositoryAsync<Supplier, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<IEnumerable<SupplierResponse>> Handle(GetSuppliersQuery query, CancellationToken cancellationToken)
        {
            var items = await _repo.NoTrackingEntities.Select(i => new SupplierResponse
            {
                Id = i.Id,
                Name = i.Name!,
                PhoneNumber = i.PhoneNumber,
                Address = i.Address,
                Active = i.Active,
            }).ToListAsync(cancellationToken);

            return items;
        }
    }
}
