using DataSets.Entity;
using DataSets.Interfaces;
using PointOfSale.Data;

namespace PointOfSale.RepositoryPattern.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetails
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetails orderDetails)
        {
            _db.Update(orderDetails);
        }
    }
}