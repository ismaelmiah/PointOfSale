using DataSets.Entity;

namespace DataSets.Interfaces
{
    public interface ISaleDetails : IRepository<SalesDetails>
    {
        void Update(SalesDetails orderDetails);
    }
}