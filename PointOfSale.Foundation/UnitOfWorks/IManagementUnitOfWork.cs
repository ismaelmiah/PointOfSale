using PointOfSale.DataAccessLayer;
using PointOfSale.Foundation.Repositories;

namespace PointOfSale.Foundation.UnitOfWorks
{
    public interface IManagementUnitOfWork : IUnitOfWork
    {
        IProductRepository ProductRepository { get; set; }
        ISaleDetailRepository SaleDetailRepository { get; set; }
        ICategoryRepository CategoryRepository { get; set; }
        IMonthDetailRepository MonthDetailRepository { get; set; }
    }
}