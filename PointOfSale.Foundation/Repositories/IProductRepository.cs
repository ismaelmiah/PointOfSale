using System;
using PointOfSale.DataAccessLayer;
using PointOfSale.Foundation.Contexts;

namespace PointOfSale.Foundation.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid, ApplicationDbcontext>
    {

    }
}