using Microsoft.EntityFrameworkCore;

namespace PointOfSale.Foundation
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories {get;set;}
        DbSet<Product> Products {get;set;}
        DbSet<MonthDetail> MonthDetails {get;set;}
        DbSet<SaleDetail> SaleDetails {get;set;}
    }
}