namespace SalesAndPurchase.Shared.Dtos.Requests
{
    public class PurchaseRequest
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
        public List<PurchaseDetailRequest>? PurchaseDetails { get; set; }
    }
}
