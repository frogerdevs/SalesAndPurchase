namespace SalesAndPurchase.Shared.Dtos.Requests
{
    public class ProductRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool Active { get; set; }
        public string CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public decimal SellPrice { get; set; }
    }
    public class PutProductRequest : ProductRequest
    {
        public required string Id { get; set; }
    }
}
