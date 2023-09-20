namespace SalesAndPurchase.Server.Application.Features.Suppliers.Queries
{
    public class GetSupplierByIdQuery : IQuery<SupplierResponse?>
    {
        public required string Id { get; set; }
    }
    public sealed class GetSupplierByIdQueryHandler : IQueryHandler<GetSupplierByIdQuery, SupplierResponse?>
    {
        private readonly IBaseRepositoryAsync<Supplier, string> _repo;

        public GetSupplierByIdQueryHandler(IBaseRepositoryAsync<Supplier, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<SupplierResponse?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
        {
            var items = await _repo.GetByIdAsync(query.Id, cancellationToken);

            return MapToResponse(items);
        }

        private static SupplierResponse? MapToResponse(Supplier? items)
        {
            return items!=null ? new SupplierResponse
            {
                Name=items.Name,
                Address=items.Address,
                PhoneNumber=items.PhoneNumber,
                Id=items.Id,
                Active=items.Active
            } : null;
        }
    }

}
