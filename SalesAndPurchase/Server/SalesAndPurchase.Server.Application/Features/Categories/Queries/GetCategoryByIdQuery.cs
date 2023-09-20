namespace SalesAndPurchase.Server.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IQuery<CategoryResponse?>
    {
        public required string Id { get; set; }
    }
    public sealed class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryResponse?>
    {
        private readonly IBaseRepositoryAsync<Category, string> _repo;

        public GetCategoryByIdQueryHandler(IBaseRepositoryAsync<Category, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<CategoryResponse?> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var items = await _repo.GetByIdAsync(query.Id, cancellationToken);

            return MapToResponse(items);
        }

        private static CategoryResponse? MapToResponse(Category? items)
        {
            return items != null ? new CategoryResponse
            {
                Id = items.Id,
                Name = items.Name,
                Description = items.Description,
                ImageUrl = items.ImageUrl,
                Active = items.Active
            } : null;
        }
    }
}
