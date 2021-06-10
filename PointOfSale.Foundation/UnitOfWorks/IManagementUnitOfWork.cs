using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation.Repositories
{
    public interface IManagementUnitOfWork : IUnitOfWork
    {
        IProductRepository ProductRepository { get; set; }
        ISaleDetailRepository SaleDetailRepository { get; set; }
        ICategoryRepository CategoryRepository { get; set; }
        IMonthDetailRepository MonthDetailRepository { get; set; }
    }
}