using DataSets.Entity;

namespace DataSets.Interfaces
{
    public interface IOrderDetails : IRepository<SalesDetails>
    {
        void Update(SalesDetails orderDetails);
    }
}