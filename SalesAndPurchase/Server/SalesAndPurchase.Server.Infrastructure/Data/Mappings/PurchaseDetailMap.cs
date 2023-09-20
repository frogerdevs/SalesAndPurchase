namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class PurchaseDetailMap : IEntityTypeConfiguration<PurchaseDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("PurchaseDetail", "sample");
        }
    }
}
