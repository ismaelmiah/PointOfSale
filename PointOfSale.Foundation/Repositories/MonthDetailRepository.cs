using System;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation.Repositories
{
    public class MonthDetailRepository : Repository<MonthDetail, Guid, ApplicationDbcontext>, IMonthDetailRepository
    {
        public MonthDetailRepository(ApplicationDbcontext context) : base(context)
        {
        }
    }
}