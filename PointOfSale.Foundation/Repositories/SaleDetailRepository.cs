using System;
using PointOfSale.DataAccessLayer;
using PointOfSale.Foundation.Contexts;

namespace PointOfSale.Foundation.Repositories
{
    public class SaleDetailRepository : Repository<SaleDetail, Guid, ApplicationDbcontext>, ISaleDetailRepository
    {
        public SaleDetailRepository(ApplicationDbcontext context) : base(context)
        {
        }
    }
}