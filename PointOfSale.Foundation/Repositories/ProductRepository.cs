using System;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation.Repositories
{
    public class ProductRepository : Repository<Product, Guid, ApplicationDbcontext>, IProductRepository
    {
        public ProductRepository(ApplicationDbcontext context) : base(context)
        {
        }
    }
}