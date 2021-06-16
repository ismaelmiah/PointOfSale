using System;
using PointOfSale.DataAccessLayer;
using PointOfSale.Foundation.Contexts;

namespace PointOfSale.Foundation.Repositories
{
    public class CategoryRepository : Repository<Category, Guid, ApplicationDbcontext>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbcontext context) : base(context)
        {
        }
    }
}