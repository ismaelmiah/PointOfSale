using DataSets.Entity;
using DataSets.Interfaces;
using PointOfSale.Data;
using System.Linq;

namespace PointOfSale.RepositoryPattern.Repository
{
    public class ProductRepo : Repository<Product>, IProduct
    {
        private readonly ApplicationDbContext _db;
        public ProductRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(Product product)
        {
            var data = _db.Products.FirstOrDefault(x => x.Id == product.Id);
            if (data == null) return;
            data.Name = product.Name;
            data.Price = product.Price;
            data.Quantity = product.Quantity;
            data.CategoryId = product.CategoryId;
        }
    }
}
