namespace SalesAndPurchase.Server.Domain.Entities
{
    public class Purchase : BaseAuditableEntity<string>
    {
        public required string SupplierId { get; set; }
        public required string SkuCode { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal SubTotal { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPriceRemaining { get; set; }
        public string? Status { get; set; }
        public ICollection<PurchaseDetail>? Purchases { get; set; }
        public Purchase()
        {
            Id  = Guid.NewGuid().ToString();
        }
    }
}
