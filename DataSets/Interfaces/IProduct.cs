using DataSets.Entity;

namespace DataSets.Interfaces
{
    public interface IProduct : IRepository<Product>
    {
        void Update(Product changeProduct);
    }
}
