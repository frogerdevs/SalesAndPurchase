namespace SalesAndPurchase.Server.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Property
        #endregion
        #region DBSet
        public DbSet<Category>? Categories { get; set; } = null!;
        public DbSet<Product>? Products { get; set; } = null!;
        public DbSet<Customer>? Customers { get; set; } = null!;
        public DbSet<Purchase>? Purchases { get; set; } = null!;
        public DbSet<PurchaseDetail>? PurchaseDetails { get; set; } = null!;
        public DbSet<PurchasePayment>? PurchasePayments { get; set; } = null!;
        public DbSet<Sell>? Sells { get; set; } = null!;
        public DbSet<SellDetail>? SellDetails { get; set; } = null!;
        public DbSet<SellPayment>? SellPayments { get; set; } = null!;
        public DbSet<Supplier>? Suppliers { get; set; } = null!;

        #endregion
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Mapping
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new SupplierMap());
            modelBuilder.ApplyConfiguration(new PurchaseMap());
            modelBuilder.ApplyConfiguration(new PurchaseDetailMap());
            modelBuilder.ApplyConfiguration(new PurchasePaymentMap());
            modelBuilder.ApplyConfiguration(new SellMap());
            modelBuilder.ApplyConfiguration(new SellDetailMap());
            modelBuilder.ApplyConfiguration(new SellPaymentMap());

            #endregion
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entitiesAdd = ChangeTracker.Entries<BaseAuditableEntity<string>>()
            .Where(e => e.State == EntityState.Added)
            .Select(e => e.Entity);

            foreach (var entity in entitiesAdd)
            {
                if (entity.CreatedDate == default)
                    entity.CreatedDate = DateTime.UtcNow;

                if (string.IsNullOrEmpty(entity.CreatedBy))
                    entity.CreatedBy = null;// GetCurrentUser();
            }
            var entitiesModif = ChangeTracker.Entries<BaseAuditableEntity<string>>()
            .Where(e => e.State == EntityState.Modified)
            .Select(e => e.Entity);

            foreach (var entity in entitiesModif)
            {
                if (entity.UpdatedDate == default)
                    entity.UpdatedDate = DateTime.UtcNow;

                if (string.IsNullOrEmpty(entity.CreatedBy))
                    entity.UpdatedBy = null;// GetCurrentUser();
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }

}
