using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Foundation.UnitOfWorks;

namespace PointOfSale.Foundation.Services
{

    public interface IProductService
    {
        void AddProduct(Product Product);
        void DeleteProduct(Guid id);
        IList<Product> Products();
        (int total, int totalDisplay, IList<Product> records) GetProductList(int pageIndex,
            int pageSize, string searchText, string orderBy);
        void UpdateProduct(Product Product);
    }

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

        public void DeleteProduct(Guid id)
        {
            _management.ProductRepository.Remove(id);
            _management.Save();
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
                result = _management.ProductRepository.GetDynamic(x => x.Price.ToString() == searchText,
                    orderBy, "Category", pageIndex, pageSize);
            }

            var data = (from x in result.data
                select new Product
                {
                    Id = x.Id,
                    Price = x.Price,
                    SaleDetail = x.SaleDetail,
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
    }
}