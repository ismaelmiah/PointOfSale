using System;
using System.Collections.Generic;

namespace PointOfSale.Foundation.Services
{
    public interface IProductService
    {
        void AddProduct(Product Product);
        bool DeleteProduct(Guid id);
        IList<Product> Products();
        (int total, int totalDisplay, IList<Product> records) GetProductList(int pageIndex,
            int pageSize, string searchText, string orderBy);
        void UpdateProduct(Product Product);
        Product GetProduct(Guid id);
    }
}