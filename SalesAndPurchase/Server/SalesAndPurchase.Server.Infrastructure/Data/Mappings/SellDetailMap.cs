namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class SellDetailMap : IEntityTypeConfiguration<SellDetail>
    {
        public void Configure(EntityTypeBuilder<SellDetail> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("SellDetail", "sample");
        }
    }
}
