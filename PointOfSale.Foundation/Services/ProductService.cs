using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Foundation.UnitOfWorks;

namespace PointOfSale.Foundation.Services
{
    public class ProductService : IProductService
    {
        private readonly IManagementUnitOfWork _management;

        public ProductService(IManagementUnitOfWork management)
        {
            _management = management;
        }
        public void AddProduct(Product Product)
        {
            _management.ProductRepository.Add(Product);
            _management.Save();
        }

        public IList<Product> Products()
        {
            return _management.ProductRepository.GetAll();
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {
                _management.ProductRepository.Remove(id);
                _management.Save();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public (int total, int totalDisplay, IList<Product> records) GetProductList(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            (IList<Product> data, int total, int totalDisplay) result;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                result = _management.ProductRepository.GetDynamic(null,
                    orderBy, "Category", pageIndex, pageSize);

            }
            else
            {
                result = _management.ProductRepository.GetDynamic(x => x.Name.ToString() == searchText,
                    orderBy, "Category", pageIndex, pageSize);
            }

            var data = (from x in result.data
                select new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    CategoryId = x.CategoryId,
                    Category = x.Category
                }).ToList();

            return (result.total, result.totalDisplay, data);
        }

        public void UpdateProduct(Product Product)
        {
            _management.ProductRepository.Edit(Product);
            _management.Save();
        }

        public Product GetProduct(Guid id) => _management.ProductRepository.GetById(id);
    }
}