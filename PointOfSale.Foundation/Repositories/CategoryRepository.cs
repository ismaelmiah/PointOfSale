using System;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation.Repositories
{
    public class CategoryRepository : Repository<Category, Guid, ApplicationDbcontext>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbcontext context) : base(context)
        {
        }
    }
}