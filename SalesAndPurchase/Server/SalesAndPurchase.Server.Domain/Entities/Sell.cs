namespace SalesAndPurchase.Server.Domain.Entities
{
    public class Sell : BaseAuditableEntity<string>
    {
        public required string SupplierId { get; set; }
        public required string SkuCode { get; set; }
        public DateTime? SalesDate { get; set; }
        public decimal SubTotal { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPriceRemaining { get; set; }
        public string? Status { get; set; }
        public ICollection<SellDetail>? SellDetails { get; set; }
        public Sell()
        {
            Id  = Guid.NewGuid().ToString();
        }

    }
}
