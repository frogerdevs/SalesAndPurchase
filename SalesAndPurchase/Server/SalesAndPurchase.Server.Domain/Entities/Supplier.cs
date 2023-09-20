namespace SalesAndPurchase.Server.Domain.Entities
{
    public class Supplier : BaseEntity<string>
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Active { get; set; }
        public Supplier()
        {
            Id  = Guid.NewGuid().ToString();
        }
    }
}
