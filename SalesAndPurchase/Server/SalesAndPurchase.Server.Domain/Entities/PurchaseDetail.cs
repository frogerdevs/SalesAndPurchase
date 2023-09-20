namespace SalesAndPurchase.Server.Domain.Entities
{
    public class PurchaseDetail : BaseAuditableEntity<string>
    {
        public required string PurchaseId { get; set; }
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Purchase? Purchase { get; set; }
        public Product? Product { get; set; }
        public PurchaseDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
