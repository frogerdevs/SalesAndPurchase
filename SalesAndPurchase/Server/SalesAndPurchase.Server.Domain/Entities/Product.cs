namespace SalesAndPurchase.Server.Domain.Entities
{
    public class Product : BaseEntity<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool Active { get; set; }
        public string CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public virtual Category? Category { get; set; }
        public Product()
        {
            Id  = Guid.NewGuid().ToString();
        }
    }
}
