using DataSets.Entity;

namespace DataSets.Interfaces
{
    public interface IOrderDetails : IRepository<OrderDetails>
    {
        void Update(OrderDetails orderDetails);
    }
}