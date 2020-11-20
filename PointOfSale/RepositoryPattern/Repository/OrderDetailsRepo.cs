using DataSets.Entity;
using DataSets.Interfaces;
using PointOfSale.Data;

namespace PointOfSale.RepositoryPattern.Repository
{
    public class OrderDetailsRepo : Repository<SalesDetails>, IOrderDetails
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepo(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SalesDetails orderDetails)
        {
            _db.Update(orderDetails);
        }
    }
}