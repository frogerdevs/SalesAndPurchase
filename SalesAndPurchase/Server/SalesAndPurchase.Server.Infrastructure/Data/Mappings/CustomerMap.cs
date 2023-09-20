namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("Customer", "sample");
        }
    }
}
