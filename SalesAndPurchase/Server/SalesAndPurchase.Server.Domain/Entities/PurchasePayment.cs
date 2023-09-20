namespace SalesAndPurchase.Server.Domain.Entities
{
    public class PurchasePayment : BaseAuditableEntity<string>
    {
        public required string PurchaseId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }
        public PurchasePayment()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
