namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class PurchaseMap : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("Purchase", "sample");
        }
    }
}
