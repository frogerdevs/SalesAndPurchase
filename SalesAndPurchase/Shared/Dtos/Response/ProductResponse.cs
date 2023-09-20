namespace SalesAndPurchase.Shared.Dtos.Response
{
    public class ProductResponse
    {
        public required string Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool Active { get; set; }
        public string? CategoryId { get; set; }
    }
}
