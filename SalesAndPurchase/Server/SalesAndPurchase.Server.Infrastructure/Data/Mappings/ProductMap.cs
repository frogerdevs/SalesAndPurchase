using SalesAndPurchase.Server.Domain.Entities;

namespace SalesAndPurchase.Server.Infrastructure.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.ToTable("Product", "sample");
            //builder.HasIndex(e => e.BrandId, "IX_Products_BrandId");
            builder.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);

            builder.Property(e => e.Id).ValueGeneratedNever();
        }
    }

}
