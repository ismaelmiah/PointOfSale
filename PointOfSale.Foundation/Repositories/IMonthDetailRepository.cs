using System;
using PointOfSale.DataAccessLayer;
using PointOfSale.Foundation.Contexts;
namespace PointOfSale.Foundation.Repositories
{
    public interface IMonthDetailRepository : IRepository<MonthDetail, Guid, ApplicationDbcontext>
    {

    }
}