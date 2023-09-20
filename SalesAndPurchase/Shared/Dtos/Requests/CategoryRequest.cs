namespace SalesAndPurchase.Shared.Dtos.Requests
{
    public class CategoryRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }
    }
    public class PutCategoryRequest : CategoryRequest
    {
        public required string Id { get; set; }
    }
}
