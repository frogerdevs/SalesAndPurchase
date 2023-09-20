namespace SalesAndPurchase.Server.Domain.Entities
{
    public class SellPayment : BaseAuditableEntity<string>
    {
        public required string PurchaseId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }
        public SellPayment()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
