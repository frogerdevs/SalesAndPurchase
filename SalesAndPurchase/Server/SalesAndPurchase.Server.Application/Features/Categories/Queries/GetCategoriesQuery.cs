namespace SalesAndPurchase.Server.Application.Features.Categories.Queries
{
    public class GetCategoriesQuery : IQuery<IEnumerable<CategoryResponse>>
    {
    }
    public sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly IBaseRepositoryAsync<Category, string> _repo;

        public GetCategoriesQueryHandler(IBaseRepositoryAsync<Category, string> repo)
        {
            _repo = repo;
        }

        public async ValueTask<IEnumerable<CategoryResponse>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var items = await _repo.Entities.AsNoTracking().Select(i => new CategoryResponse
            {
                Id = i.Id,
                Name = i.Name!,
                ImageUrl = i.ImageUrl,
                Active = i.Active,
                Description = i.Description,
            }).ToListAsync(cancellationToken);

            return items;
        }

    }

}
