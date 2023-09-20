namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class SellPaymentMap : IEntityTypeConfiguration<SellPayment>
    {
        public void Configure(EntityTypeBuilder<SellPayment> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("SellPayment", "sample");
        }
    }
}
