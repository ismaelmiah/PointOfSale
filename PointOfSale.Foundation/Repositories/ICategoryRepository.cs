using System;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid, ApplicationDbcontext>
    {
        
    }
}