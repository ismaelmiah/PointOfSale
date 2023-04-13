using Microsoft.EntityFrameworkCore;

namespace PointOfSale.Foundation.Contexts
{
    public class ApplicationDbcontext : DbContext, IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ApplicationDbcontext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_migrationAssemblyName));

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MonthDetail>()
            .HasOne(x => x.Category)
            .WithOne(x => x.MonthDetail)
            .HasForeignKey<MonthDetail>(x => x.CategoryId)
            .IsRequired();

            builder.Entity<SaleDetail>()
            .HasOne(x => x.Product)
            .WithMany(x => x.SaleDetails)
            .HasForeignKey(x => x.ProductId)
            .HasPrincipalKey(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<MonthDetail> MonthDetails { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
    }
}