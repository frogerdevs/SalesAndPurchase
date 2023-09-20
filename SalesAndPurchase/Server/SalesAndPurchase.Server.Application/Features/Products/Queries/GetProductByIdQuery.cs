namespace SalesAndPurchase.Server.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IQuery<ProductResponse?>
    {
        public required string Id { get; set; }
    }
    public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse?>
    {
        private readonly IBaseRepositoryAsync<Product, string> _repo;

        public GetProductByIdQueryHandler(IBaseRepositoryAsync<Product, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<ProductResponse?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var items = await _repo.GetByIdAsync(query.Id, cancellationToken);

            return MapToResponse(items);
        }

        private static ProductResponse? MapToResponse(Product? items)
        {
            return items!=null ? new ProductResponse
            {
                Id=items.Id,
                Code=items.Code,
                Name=items.Name,
                Price=items.Price,
                Stock=items.Stock,
                Active=items.Active,
                CategoryId=items.CategoryId
            } : null;
        }
    }

}
