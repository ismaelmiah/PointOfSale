using System;
using PointOfSale.DataAccessLayer;
using PointOfSale.Foundation.Contexts;

namespace PointOfSale.Foundation.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid, ApplicationDbcontext>
    {
        
    }
}