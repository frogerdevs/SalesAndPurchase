namespace SalesAndPurchase.Server.Domain.Entities
{
    public class Customer : BaseEntity<string>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Active { get; set; }
        public Customer()
        {
            Id  = Guid.NewGuid().ToString();
        }
    }
}
