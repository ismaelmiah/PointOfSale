using System;
using PointOfSale.DataAccessLayer;
using PointOfSale.Foundation.Contexts;

namespace PointOfSale.Foundation.Repositories
{
    public interface ISaleDetailRepository : IRepository<SaleDetail, Guid, ApplicationDbcontext>{

    }
}