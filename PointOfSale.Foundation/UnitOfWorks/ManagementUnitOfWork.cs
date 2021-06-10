using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation.Repositories
{
    public class ManagementUnitOfWork : UnitOfWork, IManagementUnitOfWork
    {
        public ManagementUnitOfWork(
            DbContext dbContext,
            IProductRepository productRepository,
            ISaleDetailRepository saleDetailRepository,
            ICategoryRepository categoryRepository,
            IMonthDetailRepository monthDetailRepository) : base(dbContext)
        {
            ProductRepository = productRepository;
            SaleDetailRepository = saleDetailRepository;
            CategoryRepository = categoryRepository;
            MonthDetailRepository = monthDetailRepository;
        }

        public IProductRepository ProductRepository { get; set; }
        public ISaleDetailRepository SaleDetailRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IMonthDetailRepository MonthDetailRepository { get; set; }
    }
}