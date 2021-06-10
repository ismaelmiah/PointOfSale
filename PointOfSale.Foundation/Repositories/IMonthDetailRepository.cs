using System;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation.Repositories
{
    public interface IMonthDetailRepository : IRepository<MonthDetail, Guid, ApplicationDbcontext>
    {

    }
}