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
            if (product.ImageUrl != null)
            {
                data.ImageUrl = product.ImageUrl;
            }
            data.Name = product.Name;
            data.Description = product.Description;
            data.BuyPrice = product.BuyPrice;
            data.SalePrice = product.SalePrice;
            data.CategoryId = product.CategoryId;
        }
    }
}
