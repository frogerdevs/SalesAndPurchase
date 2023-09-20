namespace SalesAndPurchase.Shared.Dtos.Requests
{
    public class CustomerRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

    }
    public class PutCustomerRequest : CustomerRequest
    {
        public required string Id { get; set; }
    }
}
