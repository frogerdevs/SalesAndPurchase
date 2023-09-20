using SalesAndPurchase.Shared.Dtos.Requests;

namespace SalesAndPurchase.Shared.Dtos.Response
{
    public class PurchaseResponse
    {
        public required string Id { get; set; }
        public required string SupplierId { get; set; }
        public required string SkuCode { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal SubTotal { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPriceRemaining { get; set; }
        public string? Status { get; set; }
        public IEnumerable<PurchaseDetailRequest>? PurchaseDetails { get; set; }
    }
}
