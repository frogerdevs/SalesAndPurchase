namespace SalesAndPurchase.Server.Domain.Entities
{
    public class SellDetail : BaseAuditableEntity<string>
    {
        public required string SellId { get; set; }
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Sell? Sell { get; set; }
        public Product? Product { get; set; }
        public SellDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
