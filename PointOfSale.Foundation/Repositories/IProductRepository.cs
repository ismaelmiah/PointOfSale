using System;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid, ApplicationDbcontext>
    {

    }
}