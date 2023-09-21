namespace SalesAndPurchase.Server.Application.Features.Products.Queries
{
    public class GetProductsQuery : IQuery<IEnumerable<ProductResponse>>
    {
    }
    public sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<ProductResponse>>
    {
        private readonly IBaseRepositoryAsync<Product, string> _repo;

        public GetProductsQueryHandler(IBaseRepositoryAsync<Product, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<IEnumerable<ProductResponse>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var items = await _repo.NoTrackingEntities.Select(i => new ProductResponse
            {
                Id = i.Id,
                Name = i.Name!,
                CategoryId = i.CategoryId,
                Code = i.Code,
                Price = i.Price,
                Stock = i.Stock,
                Active = i.Active,
                SellPrice = i.SellPrice
            }).ToListAsync(cancellationToken);

            return items;
        }
    }
}
