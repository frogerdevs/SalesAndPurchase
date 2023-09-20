namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class PurchasePaymentMap : IEntityTypeConfiguration<PurchasePayment>
    {
        public void Configure(EntityTypeBuilder<PurchasePayment> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("PurchasePayment", "sample");
        }
    }
}
