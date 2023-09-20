namespace SalesAndPurchase.Shared.Dtos.Response
{
    public class PurchaseDetailResponse
    {
        public required string Id { get; set; }
        public required string PurchaseId { get; set; }
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
