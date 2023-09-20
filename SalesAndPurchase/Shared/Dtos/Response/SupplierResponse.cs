namespace SalesAndPurchase.Shared.Dtos.Response
{
    public class SupplierResponse
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Id { get; set; }
        public bool Active { get; set; }
    }
}
