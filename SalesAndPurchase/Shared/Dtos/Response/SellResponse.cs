namespace SalesAndPurchase.Shared.Dtos.Response
{
    public class SellResponse
    {
        public required string SupplierId { get; set; }
        public required string SkuCode { get; set; }
        public DateTime SalesDate { get; set; }
        public decimal SubTotal { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPriceRemaining { get; set; }
        public string? Status { get; set; }
        public IEnumerable<SellDetailResponse>? SellDetails { get; set; }

    }
}
