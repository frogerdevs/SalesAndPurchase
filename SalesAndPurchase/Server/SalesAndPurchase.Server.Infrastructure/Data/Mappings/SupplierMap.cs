namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class SupplierMap : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("Supplier", "sample");
        }
    }
}
