using System;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation.Repositories
{
    public interface ISaleDetailRepository : IRepository<SaleDetail, Guid, ApplicationDbcontext>{

    }
}