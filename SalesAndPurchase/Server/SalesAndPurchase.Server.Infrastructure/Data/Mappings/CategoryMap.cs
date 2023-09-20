namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("Category", "sample");
        }
    }
}
