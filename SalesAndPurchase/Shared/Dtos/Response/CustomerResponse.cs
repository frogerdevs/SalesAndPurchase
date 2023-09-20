namespace SalesAndPurchase.Shared.Dtos.Response
{
    public class CustomerResponse
    {
        public required string Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
