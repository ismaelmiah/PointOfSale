using DataSets.Entity;
using DataSets.Interfaces;
using PointOfSale.Data;

namespace PointOfSale.RepositoryPattern.Repository
{
    public class SaleDetailsRepo : Repository<SalesDetails>, ISaleDetails
    {
        private readonly ApplicationDbContext _db;
        public SaleDetailsRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SalesDetails orderDetails)
        {
            _db.Update(orderDetails);
        }
    }
}