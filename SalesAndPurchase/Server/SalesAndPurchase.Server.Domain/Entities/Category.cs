namespace SalesAndPurchase.Server.Domain.Entities
{
    public class Category : BaseEntity<string>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }
        public ICollection<Product>? Products { get; set; }
        public Category()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
