namespace SalesAndPurchase.Shared.Dtos.Response
{
    public class SellDetailResponse
    {
        public required string Id { get; set; }
        public required string SellId { get; set; }
        public required string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
