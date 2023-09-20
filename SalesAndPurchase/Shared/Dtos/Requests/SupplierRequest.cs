namespace SalesAndPurchase.Shared.Dtos.Requests
{
    public class SupplierRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Active { get; set; }

    }
    public class PutSupplierRequest : SupplierRequest
    {
        public required string Id { get; set; }
    }
}
