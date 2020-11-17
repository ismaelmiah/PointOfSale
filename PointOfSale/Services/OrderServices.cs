using System;
using System.Collections.Generic;
using System.Globalization;
using DataSets.Entity;
using DataSets.Interfaces;

namespace PointOfSale.Services
{
    public class OrderServices
    {
        private IUnitOfWork _uow;

        public OrderServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public bool SaleProduct(Guid id, int quantity)
        {
            var product = _uow.Product.GetFirstOrDefault(x => x.Id == id & x.Quantity >= quantity);
            if (product == null) return false;
            var sale = new OrderDetails()
            {
                Count = quantity,
                Price = quantity * product.SalePrice,
                Product = product,
                SaleDate = DateTime.Now.ToString(CultureInfo.InvariantCulture)
            };
            _uow.OrderDetails.Add(sale);
            _uow.Save();

            product.Quantity -= quantity;
            _uow.Product.Update(product);
            _uow.Save();
            return true;
        }

        public bool DeleteRecord(Guid id)
        {
            var record = _uow.OrderDetails.Get(id);
            if (record == null) return false;
            _uow.OrderDetails.Remove(id);
            _uow.Save();
            var modifiedProduct = _uow.Product.Get(record.ProductId);
            modifiedProduct.Quantity += 1;
            _uow.Product.Update(modifiedProduct);
            _uow.Save();
            return true;
        }

        public IEnumerable<OrderDetails> GetAllRecords()
        {
            return _uow.OrderDetails.GetAll(includeProperties: "Product");
        }
    }
}
