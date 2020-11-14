using DataSets.Entity;

namespace DataSets.Interfaces
{
    public interface ICategory : IRepository<Category>
    {
        void Update(Category changeCategory);
    }
}