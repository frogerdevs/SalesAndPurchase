namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class SellMap : IEntityTypeConfiguration<Sell>
    {
        public void Configure(EntityTypeBuilder<Sell> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("Sell", "sample");
        }
    }
}
